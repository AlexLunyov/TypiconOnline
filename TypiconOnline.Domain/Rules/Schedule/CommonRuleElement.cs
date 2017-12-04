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
    public class CommonRuleElement : IncludingRulesElement, ICustomInterpreted//, IViewModelElement
    {
        public string CommonRuleName { get; set; }

        public CommonRuleElement(IRuleSerializerRoot serializerRoot) : base(string.Empty, serializerRoot) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Имя элемента</param>
        public CommonRuleElement(string name, IRuleSerializerRoot serializerRoot) : base(name, serializerRoot) { }


        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<CommonRuleElement>())
            {
                //находим правило
                CommonRule commonRule = handler.Settings.Rule.Owner.GetCommonRule(c => c.Name == CommonRuleName);

                var container = commonRule?.GetRule<ExecContainer>(SerializerRoot);

                if (container?.IsValid == true)
                {
                    //имеется Правило и оно верно составлено
                    //значит просто включаем его
                    container.ChildElements.ForEach(c => c.Interpret(handler));
                }
            }
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(CommonRuleName))
            {
                AddBrokenConstraint(CommonRuleBusinessConstraint.NameReqiured);
            }
        }

        //public ElementViewModel CreateViewModel(IRuleHandler handler)
        //{
        //    return new ContainerViewModel(this, handler);
        //}
    }
}
