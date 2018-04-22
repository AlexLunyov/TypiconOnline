using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.AppServices.Implementations
{
    public class ModifiedRuleService : IModifiedRuleService
    {
        IUnitOfWork unitOfWork;


        public ModifiedRuleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in ModifiedRuleService");
            //Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
        }

        //public IRuleSerializerRoot Serializer { get; }

        /// <summary>
        /// Возвращает список измененных правил для конкретной даты
        /// </summary>
        /// <param name="date">Конкретная дата</param>
        /// <returns></returns>
        public IEnumerable<ModifiedRule> GetModifiedRules(TypiconEntity typicon, DateTime date, IRuleSerializerRoot serializer)
        {
            ModifiedYear modifiedYear = typicon.ModifiedYears.FirstOrDefault(m => m.Year == date.Year);

            if (modifiedYear == null)
            {
                modifiedYear = CreateModifiedYear(typicon, date, serializer);

                //фиксируем изменения
                unitOfWork.Repository<TypiconEntity>().Update(typicon);
                unitOfWork.SaveChanges();
            }

            return modifiedYear.ModifiedRules.FindAll(d => d.Date.Date == date.Date);
        }

        private ModifiedYear CreateModifiedYear(TypiconEntity typicon, DateTime date, IRuleSerializerRoot serializer)
        {
            ModificationsRuleHandler handler = new ModificationsRuleHandler(this, date.Year);

            DateTime indexDate = new DateTime(date.Year, 1, 1);

            //формируем список дней для изменения до 1 января будущего года
            DateTime endDate = new DateTime(date.Year + 1, 1, 1);
            while (indexDate != endDate)
            {
                //Menology

                //находим правило для конкретного дня Минеи
                MenologyRule menologyRule = typicon.GetMenologyRule(indexDate);

                InterpretRule(menologyRule, indexDate, handler);

                indexDate = indexDate.AddDays(1);
            }

            //теперь обрабатываем переходящие минейные праздники
            //у них не должны быть определены даты. так их и найдем
            typicon.MenologyRules.FindAll(c => (c.Date.IsEmpty && c.DateB.IsEmpty)).
                ForEach(a =>
                {
                    InterpretRule(a, date, handler);

                    //не нашел другого способа, как только два раза вычислять изменяемые дни
                    InterpretRule(a, date.AddYears(1), handler);
                });

            //Triodion

            //найти текущую Пасху
            //Для каждого правила выполнять interpret(), где date = текущая Пасха. AddDays(Day.DaysFromEaster)
            DateTime easter = serializer.BookStorage.Easters.GetCurrentEaster(date.Year);

            typicon.TriodionRules.
                ForEach(a =>
                {
                    InterpretRule(a, easter.AddDays(a.DaysFromEaster), handler);
                });

            return typicon.ModifiedYears.FirstOrDefault(m => m.Year == date.Year);

            void InterpretRule(TypiconRule rule, DateTime dateToInterpret, ModificationsRuleHandler h)
            {
                if (rule != null)
                {
                    h.Settings = CreateSettings(rule, dateToInterpret, serializer);

                    //выполняем его
                    h.Settings.RuleContainer.Interpret(h);
                }
            }
        }

        /// <summary>
        /// Рекурсивно создает настройки для обработчика
        /// </summary>
        /// <returns></returns>
        private RuleHandlerSettings CreateSettings(TypiconRule rule, DateTime dateToInterpret, 
            IRuleSerializerRoot serializer, RuleHandlerSettings additionalSettings = null)
        {
            var settings = new RuleHandlerSettings()
            {
                TypiconRule = rule,
                RuleContainer = rule.GetRule<RootContainer>(serializer),
                Date = dateToInterpret,
                Addition = additionalSettings
            };

            if (rule.IsAddition && rule.Template != null)
            {
                settings = CreateSettings(rule.Template, dateToInterpret, serializer, settings);
            }

            return settings;
        }

        public void ClearModifiedYears(TypiconEntity typicon)
        {
            typicon.ModifiedYears.ForEach(c => c.TypiconEntity = null);
            typicon.ModifiedYears.Clear();
            //_unitOfWork.Commit();
        }

        public ModifiedRule GetModifiedRuleHighestPriority(TypiconEntity typicon, DateTime date, IRuleSerializerRoot serializer)
        {
            return GetModifiedRules(typicon, date, serializer)?.Min();
        }

        /// <summary>
        /// Добавляет измененное правило.
        /// Вызывается из метода Execute класса ModificationsRuleHandler
        /// </summary>
        /// <param name="request"></param>
        public void AddModifiedRule(TypiconEntity typicon, ModificationsRuleRequest request)
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
                UseFullName = request.UseFullName
            });
        }
    }
}
