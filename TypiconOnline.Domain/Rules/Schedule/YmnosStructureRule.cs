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
    /// Базовый класс для описания правил стихир в последовательности богослужений
    /// </summary>
    public abstract class YmnosStructureRule : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        private YmnosStructure _stichera;

        private ItemEnumType<YmnosStructureKind> _ymnosStructureKind;


        public YmnosStructureRule(XmlNode node) : base(node)
        {
            _ymnosStructureKind = new ItemEnumType<YmnosStructureKind>(node.Name);

            XmlAttribute attr = node.Attributes[RuleConstants.TotalCountAttribute];

            TotalYmnosCount = new ItemInt((attr != null) ? attr.Value : string.Empty);
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
        /// Общее количество песнопений (ограничение)
        /// </summary>
        public ItemInt TotalYmnosCount { get; set; }

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

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, structHandler);
                }

                ExecContainer container = structHandler.GetResult();

                if (container != null)
                {
                    CalculateYmnosStructure(date, handler.Settings, container);
                }

                handler.Execute(this);
            }
        }

        private void CalculateYmnosStructure(DateTime date, RuleHandlerSettings settings, ExecContainer container)
        {
            _stichera = new YmnosStructure();
            foreach (YmnosRule ymnosRule in container.ChildElements)
            {
                YmnosStructure s = ymnosRule.Calculate(date, settings) as YmnosStructure;
                if (s != null)
                {
                    switch (ymnosRule.YmnosKind.Value)
                    {
                        case YmnosRuleKind.YmnosRule:
                            _stichera.Groups.AddRange(s.Groups);
                            break;
                        case YmnosRuleKind.DoxastichonRule:
                            _stichera.Doxastichon = s.Doxastichon;
                            break;
                        case YmnosRuleKind.TheotokionRule:
                            _stichera.Theotokion = s.Theotokion;
                            break;
                    }
                }
            }
        }

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }

        public abstract ElementViewModel CreateViewModel(IRuleHandler handler);
    }
}
