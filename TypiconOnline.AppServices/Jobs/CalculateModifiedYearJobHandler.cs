using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Jobs
{
    public class CalculateModifiedYearJobHandler : ICommandHandler<CalculateModifiedYearJob>
    {
        protected readonly TypiconDBContext _dbContext;
        protected readonly IRuleHandlerSettingsFactory _settingsFactory;
        private readonly IJobRepository _jobs;

        public CalculateModifiedYearJobHandler(TypiconDBContext dbContext
            , [NotNull] IRuleHandlerSettingsFactory settingsFactory, IJobRepository jobs)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
            _jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
        }

        public virtual async Task<Result> ExecuteAsync(CalculateModifiedYearJob job)
        {
            _jobs.Start(job);

            if (_dbContext.IsModifiedYearExists(job.TypiconVersionId, job.Year))
            {
                //Значит год уже был сформирован или формируется в настоящий момент асинхронно
                string msg = $"Год {job.Year} для версии Устава={job.TypiconVersionId} уже был сформирован или формируется в настоящий момент асинхронно.";
                
                _jobs.Fail(job, msg);
                
                return Result.Fail(msg);
            }

            var modifiedYear = InnerCreate(job.TypiconVersionId, job.Year);

            //фиксируем изменения
            await _dbContext.UpdateModifiedYearAsync(modifiedYear);

            _jobs.Finish(job);

            return Result.Ok();
        }

        protected virtual ModifiedYear InnerCreate(int typiconVersionId, int year)
        {
            var modifiedYear = new ModifiedYear()
            {
                Year = year,
                TypiconVersionId = typiconVersionId,
                IsCalculated = false
            };

            //await _dbContext.UpdateModifiedYearAsync(modifiedYear);

            Fill(typiconVersionId, modifiedYear);

            modifiedYear.IsCalculated = true;

            return modifiedYear;
        }

        protected virtual void Fill(int typiconVersionId, ModifiedYear modifiedYear)
        {
            var handler = new ModificationsRuleHandler(_dbContext, typiconVersionId, modifiedYear);

            //MenologyRules

            var menologyRules = _dbContext.GetAllMenologyRules(modifiedYear.TypiconVersionId);

            EachDayPerYear.Perform(modifiedYear.Year, date =>
            {
                //находим правило для конкретного дня Минеи
                var menologyRule = menologyRules.GetMenologyRule(date);

                InterpretRule(menologyRule, date, handler);
            });

            //теперь обрабатываем переходящие минейные праздники
            //у них не должны быть определены даты. так их и найдем
            var rules = menologyRules.GetAllMovableRules();

            var firstJanuary = new DateTime(modifiedYear.Year, 1, 1);

            foreach (var a in rules)
            {
                InterpretRule(a, firstJanuary, handler);

                //не нашел другого способа, как только два раза вычислять изменяемые дни
                InterpretRule(a, firstJanuary.AddYears(1), handler);
            }

            //Triodion

            //найти текущую Пасху
            //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
            DateTime easter = _dbContext.GetCurrentEaster(modifiedYear.Year);

            var triodionRules = _dbContext.GetAllTriodionRules(modifiedYear.TypiconVersionId);

            foreach (var triodionRule in triodionRules)
            {
                InterpretRule(triodionRule, easter.AddDays(triodionRule.DaysFromEaster), handler);
            }

            void InterpretRule(DayRule rule, DateTime dateToInterpret, ModificationsRuleHandler h)
            {
                if (rule != null)
                {
                    h.ProcessingDayRule = rule;

                    var r = _settingsFactory.CreateRecursive(new CreateRuleSettingsRequest()
                    {
                        TypiconVersionId = modifiedYear.TypiconVersionId,
                        Rule = rule,
                        Date = dateToInterpret,
                        RuleMode = RuleMode.ModRule
                    });

                    if (r.Success)
                    {
                        h.Settings = r.Value;

                        //выполняем его
                        h.Settings?.RuleContainer.Interpret(h);
                    }
                }
            }
        }
    }
}
