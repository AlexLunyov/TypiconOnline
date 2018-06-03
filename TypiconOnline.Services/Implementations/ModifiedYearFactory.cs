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

        public ModifiedYearFactory(IUnitOfWork unitOfWork, IRuleSerializerRoot serializer)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in ModifiedYearFactory");
            this.serializer = serializer ?? throw new ArgumentNullException("serializer in ModifiedYearFactory");
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
            var handler = new ModificationsRuleHandler(serializer.BookStorage.RulesExtractor, modifiedYear);

            //MenologyRules
            var menologyRules = serializer.BookStorage.RulesExtractor.GetAllMenologyRules(modifiedYear.TypiconEntityId);

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
            DateTime easter = serializer.BookStorage.Easters.GetCurrentEaster(modifiedYear.Year);

            var triodionRules = serializer.BookStorage.RulesExtractor.GetAllTriodionRules(modifiedYear.TypiconEntityId);

            foreach (var triodionRule in triodionRules)
            {
                InterpretRule(triodionRule, easter.AddDays(triodionRule.DaysFromEaster), handler);
            }

            void InterpretRule(TypiconRule rule, DateTime dateToInterpret, ModificationsRuleHandler h)
            {
                if (rule != null)
                {
                    h.Settings = CreateSettings(modifiedYear.TypiconEntityId, rule, dateToInterpret, serializer);

                    //выполняем его
                    h.Settings.RuleContainer.Interpret(h);
                }
            }
        }

        /// <summary>
        /// Рекурсивно создает настройки для обработчика
        /// </summary>
        /// <returns></returns>
        private RuleHandlerSettings CreateSettings(int typiconId, TypiconRule rule, DateTime dateToInterpret,
            IRuleSerializerRoot serializer, RuleHandlerSettings additionalSettings = null)
        {
            var settings = new RuleHandlerSettings()
            {
                TypiconId = typiconId,
                TypiconRule = rule,
                RuleContainer = rule.GetRule<RootContainer>(serializer),
                Date = dateToInterpret,
                Addition = additionalSettings
            };

            if (rule.IsAddition && rule.Template != null)
            {
                settings = CreateSettings(typiconId, rule.Template, dateToInterpret, serializer, settings);
            }

            return settings;
        }

        private ModifiedYear InnerCreate(int typiconId, int year)
        {
            return new ModifiedYear() { Year = year, TypiconEntityId = typiconId };
        }
    }
}
