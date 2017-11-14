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
using TypiconOnline.Domain.Rules.Factories;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание правил для использования текстов песнопений
    /// </summary>
    public class YmnosRule : RuleExecutable, ICustomInterpreted
    {
        private ItemEnumType<YmnosRuleKind> _ymnosKind;
        private ItemEnumType<PlaceYmnosSource> _place;
        private ItemInt _count;
        private ItemInt _startFrom;
        private YmnosRule _childElement;

        public YmnosRule(XmlNode node) : base(node)
        {
            _ymnosKind = new ItemEnumType<YmnosRuleKind>(node.Name);

            XmlAttribute attr = node.Attributes[RuleConstants.YmnosRuleSourceAttrName];
            Source = (attr != null) ? new ItemEnumType<YmnosSource>(attr.Value) : null;

            attr = node.Attributes[RuleConstants.YmnosRulePlaceAttrName];
            _place = (attr != null) ? new ItemEnumType<PlaceYmnosSource>(attr.Value) : null;

            attr = node.Attributes[RuleConstants.YmnosRuleCountAttrName];
            _count = new ItemInt((attr != null) ? attr.Value : "1");

            attr = node.Attributes[RuleConstants.YmnosRuleStartFromAttrName];
            _startFrom = new ItemInt((attr != null) ? attr.Value : "1");

            if (node.HasChildNodes)
            {
                _childElement = RuleFactory.CreateYmnosRule(node.FirstChild);
            }
        }

        #region Properties

        /// <summary>
        /// Тип песнопения (общий, славник, богородичен...)
        /// </summary>
        public ItemEnumType<YmnosRuleKind> YmnosKind
        {
            get
            {
                return _ymnosKind;
            }
        }
        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public ItemEnumType<YmnosSource> Source
        {
            get; }

        /// <summary>
        /// Источник книги, откуда брать текст
        /// </summary>
        public ItemEnumType<PlaceYmnosSource> Place
        {
            get
            {
                return _place;
            }
        }
        /// <summary>
        /// Количество стихир, которые берутся из выбранного источника. По умолчанию - 1
        /// </summary>
        public ItemInt Count
        {
            get
            {
                return _count;
            }
        }
        /// <summary>
        /// Начало индекса (1 - ориентированного), начиная с которого необходимо брать стихиры выбранного источника. По умолчанию - 1
        /// </summary>
        public ItemInt StartFrom
        {
            get
            {
                return _startFrom;
            }
        }
        

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<YmnosRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (_ymnosKind == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.KindMismatch, ElementName);
            }
            else if (!_ymnosKind.IsValid)
            {
                AppendAllBrokenConstraints(_ymnosKind);
            }

            bool sourceIsValid = false;

            if (Source == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.SourceRequired, ElementName);
            }
            else if (!Source.IsValid)
            {
                AppendAllBrokenConstraints(Source);
            }
            else
            {
                //первое условие для сопоставления source и place
                sourceIsValid = true;
            }

            if (_place == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceRequired, ElementName);
            }
            else if (_place.IsValid == false)
            {
                AppendAllBrokenConstraints(_place);
            }
            else if (sourceIsValid)
            {
                /* Проверка на сопоставление source и place
                 * Если source == irmologion, то значения могут быть только сопоставимые ему, и наоборот
                */
                if ((Source.Value == YmnosSource.Irmologion)
                    && (Place.Value != PlaceYmnosSource.app1_aposticha)
                    && (Place.Value != PlaceYmnosSource.app1_kekragaria)
                    && (Place.Value != PlaceYmnosSource.app2_esperinos)
                    && (Place.Value != PlaceYmnosSource.app2_orthros)
                    && (Place.Value != PlaceYmnosSource.app3)
                    && (Place.Value != PlaceYmnosSource.app4_esperinos)
                    && (Place.Value != PlaceYmnosSource.app4_orthros))
                {
                    AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceAndSourceMismatched, ElementName);
                }
                else
                {
                    if ((Source.Value != YmnosSource.Irmologion)
                        && ((Place.Value == PlaceYmnosSource.app1_aposticha)
                            || (Place.Value == PlaceYmnosSource.app1_kekragaria)
                            || (Place.Value == PlaceYmnosSource.app2_esperinos)
                            || (Place.Value == PlaceYmnosSource.app2_orthros)
                            || (Place.Value == PlaceYmnosSource.app3)
                            || (Place.Value == PlaceYmnosSource.app4_esperinos)
                            || (Place.Value == PlaceYmnosSource.app4_orthros)))
                    {
                        AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceAndSourceMismatched, ElementName);
                    }
                }
            }

            if (_count.IsValid == false)
            {
                AppendAllBrokenConstraints(_count);
            }

            if (_startFrom.IsValid == false)
            {
                AppendAllBrokenConstraints(_startFrom);
            }
        }

        /// <summary>
        /// Возвращает указанные в правиле богослужебные тексты. 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        /// <returns>Если таковые не объявлены в DayService, возвращает NULL.</returns>
        public virtual YmnosStructure CalculateYmnosStructure(DateTime date, IRuleHandler handler)
        {
            YmnosStructure result = null;

            //разбираемся с source
            DayStructureBase dayWorship = null;
            switch (Source.Value)
            {
                case YmnosSource.Item1:
                    dayWorship = (handler.Settings.DayWorships.Count > 0) ? handler.Settings.DayWorships[0] : null;
                    break;
                case YmnosSource.Item2:
                    dayWorship = (handler.Settings.DayWorships.Count > 1) ? handler.Settings.DayWorships[1] : null;
                    break;
                case YmnosSource.Item3:
                    dayWorship = (handler.Settings.DayWorships.Count > 2) ? handler.Settings.DayWorships[2] : null;
                    break;
                case YmnosSource.Oktoikh:
                    dayWorship = handler.Settings.OktoikhDay;
                    break;
            }

            //if (dayWorship == null)
            //{
            //    throw new KeyNotFoundException("YmnosStructureRule source not found: " + Source.Value.ToString());
            //}

            //не выдаем ошибки, если день не найден
            if (dayWorship != null)
            {
                if (Place == null)
                {
                    //TODO: на случай если будет реализован функционал, когда у ymnosRule может быть не определен place
                }

                //теперь разбираемся с place И kind

                YmnosGroup group = null;
                List<YmnosGroup> groups = null;

                switch (YmnosKind.Value)
                {
                    case YmnosRuleKind.YmnosRule:
                        groups = dayWorship.GetElement().GetYmnosStructure(Place.Value, Count.Value, StartFrom.Value)?.Groups;
                        if (groups != null)
                        {
                            result = new YmnosStructure();
                            result.Groups.AddRange(groups);
                        }

                        break;
                    case YmnosRuleKind.DoxastichonRule:
                        group = dayWorship.GetElement().GetYmnosStructure(Place.Value, Count.Value, StartFrom.Value)?.Doxastichon;
                        if (group != null)
                        {
                            result = new YmnosStructure();
                            result.Doxastichon = group;
                        }

                        break;
                    case YmnosRuleKind.TheotokionRule:
                        groups = dayWorship.GetElement().GetYmnosStructure(Place.Value, Count.Value, StartFrom.Value)?.Theotokion;
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
