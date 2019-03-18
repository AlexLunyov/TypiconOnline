using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент, являющийся ссылкой на общее правило CommonRule
    /// </summary>
    public class CommonRuleElement : IncludingRulesElement//, ICustomInterpreted//, IViewModelElement
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
            //if (handler.IsAuthorized<CommonRuleElement>())
            //{
                //находим правило
                CommonRule commonRule = SerializerRoot.QueryProcessor.Process(new CommonRuleQuery(handler.Settings.TypiconVersionId, CommonRuleName));

                var container = commonRule?.GetRule<ExecContainer>(SerializerRoot);

                if (container?.IsValid == true)
                {
                    //имеется Правило и оно верно составлено
                    //значит просто включаем его
                    container.ChildElements.ForEach(c => c.Interpret(handler));
                }
            //}
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
