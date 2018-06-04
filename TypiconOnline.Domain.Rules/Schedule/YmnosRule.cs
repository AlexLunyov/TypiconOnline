using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание правил для использования текстов песнопений
    /// </summary>
    public class YmnosRule : SourceHavingRuleBase, ICustomInterpreted, IYmnosStructureRuleElement
    {
        public YmnosRule(string name, ITypiconSerializer serializer, IWeekDayAppContext weekDayAppContext) 
            : base(name, serializer, weekDayAppContext)
        {

        }

        #region Properties

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public PlaceYmnosSource Place { get; set; }

        /// <summary>
        /// Место для вставки песнопения
        /// </summary>
        public YmnosRuleKind Kind { get; set; }

        /// <summary>
        /// Количество стихир, которые берутся из выбранного источника. По умолчанию - 1
        /// </summary>
        public int Count { get; set; } = 1;

        /// <summary>
        /// Начало индекса (1 - ориентированного), начиная с которого необходимо брать стихиры выбранного источника. По умолчанию - 1
        /// </summary>
        public int StartFrom { get; set; } = 1;

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<YmnosRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            /* Проверка на сопоставление source и place
             * Если source == WeekDay, то значения могут быть только сопоставимые ему
            */
            if ((Source == YmnosSource.WeekDay)
                && (Place != PlaceYmnosSource.troparion)
                && (Place != PlaceYmnosSource.troparion_doxastichon)
                && (Place != PlaceYmnosSource.troparion_theotokion))
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceAndSourceMismatched, ElementName);
            }

            if (Count < 0)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.InvalidCount, ElementName);
            }

            if (StartFrom < 1)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.InvalidStartFrom, ElementName);
            }
        }

        /// <summary>
        /// Возвращает указанные в правиле богослужебные тексты. 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>Если таковые не объявлены в DayWorship, возвращает NULL.</returns>
        public YmnosStructure GetStructure(RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            if (!ThrowExceptionIfInvalid(settings))
            {
                //разбираемся с source
                DayContainer day = GetDayContainer(settings);

                //не выдаем ошибки, если день не найден
                if (day != null)
                {
                    //теперь разбираемся с place И kind
                    switch (Kind)
                    {
                        case YmnosRuleKind.Ymnos:
                            {
                                var groups = day.GetYmnosGroups(Place, Count, StartFrom);
                                if (groups != null)
                                {
                                    result = new YmnosStructure();
                                    result.Groups.AddRange(groups);
                                }
                            }
                            break;
                        case YmnosRuleKind.Doxastichon:
                            {
                                var groups = day.GetYmnosGroups(Place, 1, StartFrom);
                                if (groups?.Count > 0)
                                {
                                    result = new YmnosStructure { Doxastichon = groups[0] };
                                }
                            }
                            break;
                        case YmnosRuleKind.Theotokion:
                            {
                                var groups = day.GetYmnosGroups(Place, 1, StartFrom);
                                if (groups?.Count > 0)
                                {
                                    result = new YmnosStructure();
                                    result.Theotokion.AddRange(groups);
                                }
                            }
                            break;
                    }
                }
            }

            return result;
        }

        public override DayElementBase Calculate(RuleHandlerSettings settings) => GetStructure(settings);
    }
}
