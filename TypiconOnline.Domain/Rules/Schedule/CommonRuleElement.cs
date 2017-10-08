using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент, являющийся ссылкой на общее правило CommonRule
    /// </summary>
    public class CommonRuleElement : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        public string Name { get; set; }

        //public List<RuleElement> ChildElements { get; set; }

        public CommonRuleElement(XmlNode node) : base(node)
        {
            ChildElements = new List<RuleElement>();

            Name = node.Attributes[RuleConstants.CommonRuleNameAttr]?.Value;
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<CommonRuleElement>())
            {
                //находим правило
                CommonRule commonRule = handler.Settings.Rule.Owner.GetCommonRule(c => c.Name == Name);

                if (commonRule?.Rule?.IsValid == true && commonRule?.Rule is ExecContainer)
                {
                    //имеется Правило и оно верно составлено
                    //значит просто включаем его
                    ChildElements = (commonRule.Rule as ExecContainer).ChildElements.ToList();

                    ChildElements.ForEach(c => c.Interpret(date, handler));
                }
                
            }
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(CommonRuleBusinessConstraint.NameReqiured);
            }
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new ContainerViewModel(this, handler);
        }
    }
}
