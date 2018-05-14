using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание правил для использования текстов песнопений
    /// </summary>
    public class YmnosRule : YmnosRuleBase, ICustomInterpreted
    {
        public YmnosRule(string name) : base(name) { }

        #region Properties

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public YmnosSource? Source { get; set; }
        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public PlaceYmnosSource? Place { get; set; }
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
            //if (_ymnosKind == null)
            //{
            //    AddBrokenConstraint(YmnosRuleBusinessConstraint.KindMismatch, ElementName);
            //}
            //else if (!_ymnosKind.IsValid)
            //{
            //    AppendAllBrokenConstraints(_ymnosKind);
            //}

            //bool sourceIsValid = false;

            if (Source == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.SourceRequired, ElementName);
            }
            //else if (!Source.IsValid)
            //{
            //    AppendAllBrokenConstraints(Source);
            //}
            //else
            //{
            //    //первое условие для сопоставления source и place
            //    sourceIsValid = true;
            //}

            if (Place == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceRequired, ElementName);
            }
            //else if (_place.IsValid == false)
            //{
            //    AppendAllBrokenConstraints(_place);
            //}
            //else if (sourceIsValid)
            //{
            /* Проверка на сопоставление source и place
             * Если source == irmologion, то значения могут быть только сопоставимые ему, и наоборот
            */
            if ((Source == YmnosSource.Irmologion)
                    && (Place != PlaceYmnosSource.app1_aposticha)
                    && (Place != PlaceYmnosSource.app1_kekragaria)
                    && (Place != PlaceYmnosSource.app2_esperinos)
                    && (Place != PlaceYmnosSource.app2_orthros)
                    && (Place != PlaceYmnosSource.app3)
                    && (Place != PlaceYmnosSource.app4_esperinos)
                    && (Place != PlaceYmnosSource.app4_orthros))
                {
                    AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceAndSourceMismatched, ElementName);
                }
                else
                {
                    if ((Source != YmnosSource.Irmologion)
                        && ((Place == PlaceYmnosSource.app1_aposticha)
                            || (Place == PlaceYmnosSource.app1_kekragaria)
                            || (Place == PlaceYmnosSource.app2_esperinos)
                            || (Place == PlaceYmnosSource.app2_orthros)
                            || (Place == PlaceYmnosSource.app3)
                            || (Place == PlaceYmnosSource.app4_esperinos)
                            || (Place == PlaceYmnosSource.app4_orthros)))
                    {
                        AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceAndSourceMismatched, ElementName);
                    }
                }
            //}

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
        /// <param name="date"></param>
        /// <param name="handler"></param>
        /// <returns>Если таковые не объявлены в DayService, возвращает NULL.</returns>
        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            if (!IsValid)
            {
                return null;
            }

            YmnosStructure result = null;

            //разбираемся с source
            DayStructureBase dayWorship = null;
            switch (Source)
            {
                case YmnosSource.Item1:
                    dayWorship = (settings.DayWorships.Count > 0) ? settings.DayWorships[0] : null;
                    break;
                case YmnosSource.Item2:
                    dayWorship = (settings.DayWorships.Count > 1) ? settings.DayWorships[1] : null;
                    break;
                case YmnosSource.Item3:
                    dayWorship = (settings.DayWorships.Count > 2) ? settings.DayWorships[2] : null;
                    break;
                case YmnosSource.Oktoikh:
                    dayWorship = settings.OktoikhDay;
                    break;
            }

            //if (dayWorship == null)
            //{
            //    throw new KeyNotFoundException("YmnosStructureRule source not found: " + Source.ToString());
            //}

            //не выдаем ошибки, если день не найден
            if (dayWorship != null)
            {
                //теперь разбираемся с place И kind

                YmnosGroup group = null;
                List<YmnosGroup> groups = null;

                switch (Kind)
                {
                    case YmnosRuleKind.YmnosRule:
                        groups = dayWorship.GetElement().GetYmnosStructure(Place.Value, Count, StartFrom)?.Groups;
                        if (groups != null)
                        {
                            result = new YmnosStructure();
                            result.Groups.AddRange(groups);
                        }

                        break;
                    case YmnosRuleKind.DoxastichonRule:
                        group = dayWorship.GetElement().GetYmnosStructure(Place.Value, Count, StartFrom)?.Doxastichon;
                        if (group != null)
                        {
                            result = new YmnosStructure
                            {
                                Doxastichon = group
                            };
                        }

                        break;
                    case YmnosRuleKind.TheotokionRule:
                        groups = dayWorship.GetElement().GetYmnosStructure(Place.Value, Count, StartFrom)?.Theotokion;
                        if (groups != null)
                        {
                            result = new YmnosStructure();
                            result.Theotokion.AddRange(groups);
                        }

                        break;
                }
            }

            return result;
        }
    }

    
}
