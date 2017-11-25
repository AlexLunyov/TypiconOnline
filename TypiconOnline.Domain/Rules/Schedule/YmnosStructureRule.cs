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
        public YmnosStructureRule(string name) : base(name) { }

        #region Properties

        /// <summary>
        /// Тип структуры (Господи воззвах, стихиры на стиховне и т.д.)
        /// </summary>
        public YmnosStructureKind Kind { get; set; }

        /// <summary>
        /// Общее количество песнопений (ограничение)
        /// </summary>
        public int TotalYmnosCount { get; set; }

        /// <summary>
        /// Вычисленная последовательность богослужебных текстов
        /// </summary>
        public YmnosStructure Structure { get; private set; }

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
            Structure = new YmnosStructure();
            foreach (YmnosRule ymnosRule in container.ChildElements)
            {
                if (ymnosRule.Calculate(date, settings) is YmnosStructure s)
                {
                    switch (ymnosRule.Kind)
                    {
                        case YmnosRuleKind.YmnosRule:
                            Structure.Groups.AddRange(s.Groups);
                            break;
                        case YmnosRuleKind.DoxastichonRule:
                            Structure.Doxastichon = s.Doxastichon;
                            break;
                        case YmnosRuleKind.TheotokionRule:
                            Structure.Theotokion = s.Theotokion;
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
