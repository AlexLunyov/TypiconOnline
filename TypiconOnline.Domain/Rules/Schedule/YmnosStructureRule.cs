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
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание стихир Господи воззвах в последовательности богослужений
    /// </summary>
    public class YmnosStructureRule : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        private YmnosStructure _stichera;

        private ItemEnumType<YmnosStructureKind> _ymnosStructureKind;


        public YmnosStructureRule(XmlNode node) : base(node)
        {
            _ymnosStructureKind = new ItemEnumType<YmnosStructureKind>(node.Name);
        }

        #region Properties

        /// <summary>
        /// Тип структуры (Господи воззвах, стихиры на стиховне и т.д.)
        /// </summary>
        public ItemEnumType<YmnosStructureKind> YmnosStructureKind
        {
            get
            {
                return _ymnosStructureKind;
            }
        }

        /// <summary>
        /// Вычисленная последовательность богослужебных текстов
        /// </summary>
        public YmnosStructure CalculatedYmnosStructure
        {
            get
            {
                return _stichera;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            ThrowExceptionIfInvalid();

            if (handler.IsAuthorized<YmnosStructureRule>())
            {
                //используем специальный обработчик для YmnosStructureRule,
                //чтобы создать список источников стихир на обработку
                YmnosStructureRuleHandler structHandler = new YmnosStructureRuleHandler();

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, structHandler);
                }

                ExecContainer container = structHandler.GetResult();

                if (container != null)
                {
                    CalculateYmnosStructure(date, handler, container);
                }

                handler.Execute(this);
            }
        }

        private void CalculateYmnosStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            _stichera = new YmnosStructure();
            foreach (YmnosRule ymnosRule in container.ChildElements)
            {
                if (ymnosRule.Source.Value == YmnosSource.Irmologion)
                {
                    //TODO: добавляем Богородичны из приложений Ирмология
                }
                else
                {
                    //разбираемся с source
                    DayService dayService = null;
                    switch (ymnosRule.Source.Value)
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

                    if (dayService == null)
                    {
                        throw new KeyNotFoundException("YmnosStructureRule source not found: " + ymnosRule.Source.Value.ToString());
                    }

                    if (ymnosRule.Place == null)
                    {
                        //TODO: на случай если будет реализован функционал, когда у ymnosRule может быть не определен place
                    }

                    //теперь разбираемся с place И kind

                    //TODO: закомментил все, потому как dayService.GetYmnosStructure не работает
                    //switch (ymnosRule.YmnosKind.Value)
                    //{
                    //    case YmnosKind.Ymnos:
                    //        _stichera.Groups.AddRange(dayService.GetYmnosStructure(ymnosRule.Place.Value, ymnosRule.Count.Value, ymnosRule.StartFrom.Value).Groups);
                    //        break;
                    //    case YmnosKind.Doxastichon:
                    //        _stichera.Doxastichon = dayService.GetYmnosStructure(ymnosRule.Place.Value, ymnosRule.Count.Value, ymnosRule.StartFrom.Value).Doxastichon;
                    //        break;
                    //    case YmnosKind.Theotokion:
                    //        _stichera.Theotokion = dayService.GetYmnosStructure(ymnosRule.Place.Value, ymnosRule.Count.Value, ymnosRule.StartFrom.Value).Theotokion;
                    //        break;
                    //}
                    
                }
            }

            SetStringCommonRules(handler);
        }

        private void SetStringCommonRules(IRuleHandler handler)
        {
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { Handler = handler };

            req.Key = CommonRuleConstants.IhosText;
            ItemText ihosText = CommonRuleService.Instance.GetItemTextValue(req);

            //добавляем стихи к славнику и богородичну
            if (_stichera.Doxastichon != null)
            {
                //слава
                req.Key = CommonRuleConstants.SlavaText;
                _stichera.Doxastichon.Ymnis[0].Stihoi.Add(CommonRuleService.Instance.GetItemTextValue(req));

                //и ныне
                if (_stichera.Theotokion?.Count > 0)
                {
                    req.Key = CommonRuleConstants.InyneText;
                    _stichera.Theotokion[0].Ymnis[0].Stihoi.Add(CommonRuleService.Instance.GetItemTextValue(req));
                }
            }
            else
            {
                //слава и ныне
                if (_stichera.Theotokion?.Count > 0)
                {
                    req.Key = CommonRuleConstants.SlavaInyneText;
                    _stichera.Theotokion[0].Ymnis[0].Stihoi.Add(CommonRuleService.Instance.GetItemTextValue(req));
                }
            }
        }

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new YmnosStructureViewModel(this, handler);
        }
    }
}
