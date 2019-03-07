using JetBrains.Annotations;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Implementations.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Schedule;

namespace TypiconOnline.AppServices.Jobs
{
    public class CreateModifiedYearJobHandler : ICommandHandler<CreateModifiedYearJob>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IRuleSerializerRoot _serializer;
        private readonly IRuleHandlerSettingsFactory _settingsFactory;
        private readonly ITypiconFacade _facade;

        public CreateModifiedYearJobHandler([NotNull] IUnitOfWork unitOfWork
            //, [NotNull] IRuleSerializerRoot serializer
            , [NotNull] IRuleHandlerSettingsFactory settingsFactory
            , [NotNull] ITypiconFacade facade)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            //_serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _settingsFactory = settingsFactory ?? throw new ArgumentNullException(nameof(settingsFactory));
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }

        public void Execute([NotNull] CreateModifiedYearJob command)
        {
            var modifiedYear = InnerCreate(command.TypiconId, command.Year);

            Fill(modifiedYear);

            //фиксируем изменения
            _unitOfWork.Repository<ModifiedYear>().Add(modifiedYear);
            _unitOfWork.SaveChanges();
        }

        private void Fill(ModifiedYear modifiedYear)
        {
            var handler = new ModificationsRuleHandler(_facade, modifiedYear);

            //MenologyRules
            var menologyRules = _facade.GetAllMenologyRules(modifiedYear.TypiconVersionId);

            DateTime firstJanuary = new DateTime(modifiedYear.Year, 1, 1);

            DateTime indexDate = firstJanuary;

            //формируем список дней для изменения до 1 января будущего года
            DateTime endDate = firstJanuary.AddYears(1);
            while (indexDate != endDate)
            {
                //находим правило для конкретного дня Минеи
                var menologyRule = menologyRules.GetMenologyRule(indexDate);

                InterpretRule(menologyRule, indexDate, handler);

                indexDate = indexDate.AddDays(1);
            }

            //теперь обрабатываем переходящие минейные праздники
            //у них не должны быть определены даты. так их и найдем

            var rules = menologyRules.Where(c => (c.Date.IsEmpty && c.LeapDate.IsEmpty));

            foreach (var a in rules)
            {
                InterpretRule(a, firstJanuary, handler);

                //не нашел другого способа, как только два раза вычислять изменяемые дни
                InterpretRule(a, firstJanuary.AddYears(1), handler);
            }

            //Triodion

            //найти текущую Пасху
            //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
            DateTime easter = _facade.GetCurrentEaster(modifiedYear.Year);

            var triodionRules = _facade.GetAllTriodionRules(modifiedYear.TypiconVersionId);

            foreach (var triodionRule in triodionRules)
            {
                InterpretRule(triodionRule, easter.AddDays(triodionRule.DaysFromEaster), handler);
            }

            void InterpretRule(DayRule rule, DateTime dateToInterpret, ModificationsRuleHandler h)
            {
                if (rule != null)
                {
                    h.ProcessingDayRule = rule;

                    h.Settings = _settingsFactory.Create(new CreateRuleSettingsRequest()
                    {
                        TypiconId = modifiedYear.TypiconVersionId,
                        Rule = rule,
                        Date = dateToInterpret
                    });

                    //выполняем его
                    h.Settings?.RuleContainer.Interpret(h);
                }
            }
        }

        private ModifiedYear InnerCreate(int typiconId, int year)
        {
            return new ModifiedYear() { Year = year, TypiconVersionId = typiconId };
        }
    }
}
