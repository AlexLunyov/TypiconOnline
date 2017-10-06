using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание правил для использования текстов песнопений
    /// </summary>
    public class YmnosRule : RuleExecutable, ICustomInterpreted
    {
        private ItemEnumType<YmnosRuleKind> _ymnosKind;
        private ItemEnumType<YmnosSource> _source;
        private ItemEnumType<PlaceYmnosSource> _place;
        private ItemInt _count;
        private ItemInt _startFrom;
        private YmnosRule _childElement;

        public YmnosRule(XmlNode node) : base(node)
        {
            _ymnosKind = new ItemEnumType<YmnosRuleKind>(node.Name);

            XmlAttribute attr = node.Attributes[RuleConstants.YmnosRuleSourceAttrName];
            _source = (attr != null) ? new ItemEnumType<YmnosSource>(attr.Value) : null;

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
            get
            {
                return _source;
            }
        }

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
            if (IsValid && handler.IsAuthorized<YmnosRule>())
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

            if (_source == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.SourceRequired, ElementName);
            }
            else if (!_source.IsValid)
            {
                AppendAllBrokenConstraints(_source);
            }

            if (_place == null)
            {
                AddBrokenConstraint(YmnosRuleBusinessConstraint.PlaceRequired, ElementName);
            }
            else if (_place.IsValid == false)
            {
                AppendAllBrokenConstraints(_place);
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
        public YmnosStructure CalculateYmnosStructure(DateTime date, IRuleHandler handler)
        {
            //ThrowExceptionIfInvalid();

            YmnosStructure result = null;

            if (Source.Value == YmnosSource.Irmologion)
            {
                //TODO: добавляем Богородичны из приложений Ирмология
            }
            else
            {
                //разбираемся с source
                DayService dayService = null;
                switch (Source.Value)
                {
                    case YmnosSource.Item1:
                        dayService = (handler.Settings.DayServices.Count > 0) ? handler.Settings.DayServices[0] : null;
                        break;
                    case YmnosSource.Item2:
                        dayService = (handler.Settings.DayServices.Count > 1) ? handler.Settings.DayServices[1] : null;
                        break;
                    case YmnosSource.Item3:
                        dayService = (handler.Settings.DayServices.Count > 2) ? handler.Settings.DayServices[2] : null;
                        break;
                    case YmnosSource.Oktoikh:
                        dayService = BookStorage.Oktoikh.GetOktoikhDay(date);
                        break;
                }

                //if (dayService == null)
                //{
                //    throw new KeyNotFoundException("YmnosStructureRule source not found: " + Source.Value.ToString());
                //}

                //не выдаем ошибки, если день не найден
                if (dayService != null)
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
                        case YmnosRuleKind.Ymnos:
                            groups = dayService.GetDay().GetYmnosStructure(Place.Value, Count.Value, StartFrom.Value)?.Groups;
                            if (groups != null)
                            {
                                result = new YmnosStructure();
                                result.Groups.AddRange(groups);
                            }

                            break;
                        case YmnosRuleKind.Doxastichon:
                            group = dayService.GetDay().GetYmnosStructure(Place.Value, Count.Value, StartFrom.Value)?.Doxastichon;
                            if (group != null)
                            {
                                result = new YmnosStructure();
                                result.Doxastichon = group;
                            }

                            break;
                        case YmnosRuleKind.Theotokion:
                            groups = dayService.GetDay().GetYmnosStructure(Place.Value, Count.Value, StartFrom.Value)?.Theotokion;
                            if (group != null)
                            {
                                result = new YmnosStructure();
                                result.Theotokion.AddRange(groups);
                            }

                            break;
                    }
                }
            }

            return result;
        }
    }

    
}
