using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Command.Typicon;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class ModifiedYearFactory : IModifiedYearFactory
    {
        private readonly TypiconDBContext _dbContext;
        protected readonly IRuleHandlerSettingsFactory _settingsFactory;

        public ModifiedYearFactory(TypiconDBContext dbContext
            , [NotNull] IRuleHandlerSettingsFactory settingsFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
        }


        public ModifiedYear Create(int typiconVersionId, int year)
        {
            var modifiedYear = new ModifiedYear() { Year = year, TypiconVersionId = typiconVersionId };

            Fill(typiconVersionId, modifiedYear);

            //фиксируем изменения
            _dbContext.UpdateModifiedYearAsync(modifiedYear);

            return modifiedYear;
        }

        private void Fill(int typiconVersionId, ModifiedYear modifiedYear)
        {
            var handler = new ModificationsRuleHandler(_dbContext, typiconVersionId, modifiedYear);

            //MenologyRules

            //формируем список дней для изменения до 1 января будущего года

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

            DateTime firstJanuary = new DateTime(modifiedYear.Year, 1, 1);

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

                    h.Settings = _settingsFactory.CreateRecursive(new CreateRuleSettingsRequest()
                    {
                        TypiconVersionId = modifiedYear.TypiconVersionId,
                        Rule = rule,
                        Date = dateToInterpret
                    });

                    //выполняем его
                    h.Settings?.RuleContainer.Interpret(h);
                }
            }
        }
    }
}
