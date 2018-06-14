using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    public class ModifiedYearFactory : IModifiedYearFactory
    {
        IUnitOfWork unitOfWork;
        IRuleSerializerRoot serializer;
        IRuleHandlerSettingsFactory settingsFactory;

        public ModifiedYearFactory(IUnitOfWork unitOfWork, IRuleSerializerRoot serializer, IRuleHandlerSettingsFactory settingsFactory)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in ModifiedYearFactory");
            this.serializer = serializer ?? throw new ArgumentNullException("serializer in ModifiedYearFactory");
            this.settingsFactory = settingsFactory ?? throw new ArgumentNullException("settingsFactory in ModifiedYearFactory");
        }

        public ModifiedYear Create(int typiconId, int year)
        {
            //ModifiedYear modifiedYear = unitOfWork.Repository<ModifiedYear>().Get(m => m.TypiconEntityId == typiconId && m.Year == year);

            //if (modifiedYear == null)
            //{
                var modifiedYear = InnerCreate(typiconId, year);

                Fill(modifiedYear);

                //фиксируем изменения
                unitOfWork.Repository<ModifiedYear>().Update(modifiedYear);
                unitOfWork.SaveChanges();
            //}

            return modifiedYear;
        }

        private void Fill(ModifiedYear modifiedYear)
        {
            var handler = new ModificationsRuleHandler(serializer.QueryProcessor, modifiedYear);

            //MenologyRules
            var menologyRules = serializer.QueryProcessor.Process(new AllMenologyRulesQuery(modifiedYear.TypiconEntityId));

            DateTime firstJanuary = new DateTime(modifiedYear.Year, 1, 1);

            DateTime indexDate = firstJanuary;

            //формируем список дней для изменения до 1 января будущего года
            DateTime endDate = firstJanuary.AddYears(1);
            while (indexDate != endDate)
            {
                //Menology

                //находим правило для конкретного дня Минеи
                MenologyRule menologyRule = menologyRules.GetMenologyRule(indexDate);

                InterpretRule(menologyRule, indexDate, handler);

                indexDate = indexDate.AddDays(1);
            }

            //теперь обрабатываем переходящие минейные праздники
            //у них не должны быть определены даты. так их и найдем
            var rules = menologyRules.Where(c => (c.Date.IsEmpty && c.DateB.IsEmpty));

            foreach (var a in rules)
            {
                InterpretRule(a, firstJanuary, handler);

                //не нашел другого способа, как только два раза вычислять изменяемые дни
                InterpretRule(a, firstJanuary.AddYears(1), handler);
            }

            //Triodion

            //найти текущую Пасху
            //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
            DateTime easter = serializer.QueryProcessor.Process(new CurrentEasterQuery(modifiedYear.Year));

            var triodionRules = serializer.QueryProcessor.Process(new AllTriodionRulesQuery(modifiedYear.TypiconEntityId));

            foreach (var triodionRule in triodionRules)
            {
                InterpretRule(triodionRule, easter.AddDays(triodionRule.DaysFromEaster), handler);
            }

            void InterpretRule(DayRule rule, DateTime dateToInterpret, ModificationsRuleHandler h)
            {
                if (rule != null)
                {
                    h.ProcessingDayRule = rule;

                    h.Settings = settingsFactory.Create(new GetRuleSettingsRequest()
                    {
                        TypiconId = modifiedYear.TypiconEntityId,
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
            return new ModifiedYear() { Year = year, TypiconEntityId = typiconId };
        }
    }
}
