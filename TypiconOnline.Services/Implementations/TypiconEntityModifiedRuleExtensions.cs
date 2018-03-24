using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Расширения для TypiconEntity, касающиеся обработки измененных правил
    /// </summary>
    public static class TypiconModifiedRuleExtensions //: IModifiedRuleService
    {
        /// <summary>
        /// Возвращает список измененных правил для конкретной даты
        /// </summary>
        /// <param name="date">Конкретная дата</param>
        /// <returns></returns>
        public static IEnumerable<ModifiedRule> GetModifiedRules(this TypiconEntity typicon, DateTime date, IRuleSerializerRoot serializer)
        {
            ModifiedYear modifiedYear = typicon.ModifiedYears.FirstOrDefault(m => m.Year == date.Year);

            if (modifiedYear == null)
            {
                modifiedYear = CreateModifiedYear(typicon, date, serializer);

                //typicon.ModifiedYears.Add(modifiedYear);

                //_unitOfWork.Commit();
            }

            return modifiedYear.ModifiedRules.Where(d => d.Date.Date == date.Date);
        }

        private static ModifiedYear CreateModifiedYear(TypiconEntity typicon, DateTime date, IRuleSerializerRoot serializer)
        {
            ModificationsRuleHandler handler = new ModificationsRuleHandler(date.Year);

            DateTime indexDate = new DateTime(date.Year, 1, 1);

            //формируем список дней для изменения до 1 января будущего года
            DateTime endDate = new DateTime(date.Year + 1, 1, 1);
            while (indexDate != endDate)
            {
                //Menology

                //находим правило для конкретного дня Минеи
                MenologyRule menologyRule = typicon.GetMenologyRule(indexDate);

                if (menologyRule == null)
                    throw new ArgumentNullException("MenologyRule");

                InterpretMenologyRule(menologyRule, indexDate, handler);

                indexDate = indexDate.AddDays(1);
            }

            //теперь обрабатываем переходящие минейные праздники
            //у них не должны быть определены даты. так их и найдем

            typicon.MenologyRules.FindAll(c => (c.Date.IsEmpty && c.DateB.IsEmpty)).
                ForEach(a =>
                {
                    InterpretMenologyRule(a, date, handler);

                    //не нашел другого способа, как только два раза вычислять изменяемые дни
                    InterpretMenologyRule(a, date.AddYears(1), handler);
                });

            //Triodion

            //найти текущую Пасху
            //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
            DateTime easter = serializer.BookStorage.Easters.GetCurrentEaster(date.Year);

            typicon.TriodionRules.
                ForEach(a =>
                {
                    handler.Settings.Rule = a;
                    handler.Settings.Date = easter.AddDays(a.DaysFromEaster);

                    a.GetRule(serializer)?.Interpret(handler);
                    //RuleElement rule = a.GetRule(serializer);
                    //if (rule != null)
                    //{
                    //    ModificationsRuleHandler handler = new ModificationsRuleHandler(
                    //        new RuleHandlerSettings(a, easter.AddDays(a.DaysFromEaster)), date.Year);

                    //    rule.Interpret(handler);
                    //}
                });

            return typicon.ModifiedYears.FirstOrDefault(m => m.Year == date.Year); ;

            void InterpretMenologyRule(MenologyRule menologyRule, DateTime dateToInterpret, /*int year, */ModificationsRuleHandler h)
            {
                if (menologyRule != null)
                {
                    h.Settings.Rule = menologyRule;
                    h.Settings.Date = dateToInterpret;

                    //ModificationsRuleHandler handler = new ModificationsRuleHandler(
                    //    new RuleHandlerSettings(, dateToInterpret), year);
                    //выполняем его
                    menologyRule.GetRule(serializer)?.Interpret(h);
                }
            }
        }

        public static void ClearModifiedYears(this TypiconEntity typicon)
        {
            typicon.ModifiedYears.ForEach(c => c.TypiconEntity = null);
            typicon.ModifiedYears.Clear();
            //_unitOfWork.Commit();
        }

        public static ModifiedRule GetModifiedRuleHighestPriority(this TypiconEntity typicon, DateTime date, IRuleSerializerRoot serializer)
        {
            return GetModifiedRules(typicon, date, serializer)?.Min();
        }

        /// <summary>
        /// Добавляет измененное правило.
        /// Вызывается из метода Execute класса ModificationsRuleHandler
        /// </summary>
        /// <param name="request"></param>
        public static void AddModifiedRule(this TypiconEntity typicon, ModificationsRuleRequest request)
        {
            ModifiedYear modifiedYear = typicon.ModifiedYears.FirstOrDefault(m => m.Year == request.Date.Year);

            if (modifiedYear == null)
            {
                modifiedYear = new ModifiedYear() { Year = request.Date.Year, TypiconEntity = typicon };
                typicon.ModifiedYears.Add(modifiedYear);
            }

            //ModifiedRule

            modifiedYear.ModifiedRules.Add(new ModifiedRule()
            {
                Date = request.Date,
                RuleEntity = request.Caller,
                Priority = request.Priority,
                IsLastName = request.IsLastName,
                IsAddition = request.AsAddition,
                ShortName = request.ShortName,
                UseFullName = request.UseFullName,
                SignNumber = request.SignNumber,
                Filter = request.Filter
            });
        }
    }
}
