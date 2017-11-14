using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для составления канонов
    /// </summary>
    public class KanonasRule : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        public KanonasRule(XmlNode node) : base(node)
        {

        }

        #region Properties


        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasRule>())
            {
                //используем специальный обработчик для YmnosStructureRule,
                //чтобы создать список источников стихир на обработку
                CollectorRuleHandler<KanonasItem> structHandler = new CollectorRuleHandler<KanonasItem>() { Settings = handler.Settings };

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, structHandler);
                }

                ExecContainer container = structHandler.GetResult();

                if (container != null)
                {
                    CalculateKanonasStructure(date, handler, container);
                }

                handler.Execute(this);
            }
        }

        private void CalculateKanonasStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            throw new NotImplementedException();
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
