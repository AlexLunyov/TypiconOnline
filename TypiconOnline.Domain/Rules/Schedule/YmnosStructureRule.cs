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
            if (handler.IsAuthorized<YmnosStructureRule>())
            {
                //используем специальный обработчик для YmnosStructureRule,
                //чтобы создать список источников стихир на обработку
                CollectorRuleHandler<YmnosRule> structHandler = new CollectorRuleHandler<YmnosRule>() { Settings = handler.Settings };
                //YmnosStructureRuleHandler structHandler = new YmnosStructureRuleHandler();

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
                YmnosStructure s = ymnosRule.CalculateYmnosStructure(date, handler);
                if (s != null)
                {
                    switch (ymnosRule.YmnosKind.Value)
                    {
                        case YmnosRuleKind.Ymnos:
                            _stichera.Groups.AddRange(s.Groups);
                            break;
                        case YmnosRuleKind.Doxastichon:
                            _stichera.Doxastichon = s.Doxastichon;
                            break;
                        case YmnosRuleKind.Theotokion:
                            _stichera.Theotokion = s.Theotokion;
                            break;
                    }
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
