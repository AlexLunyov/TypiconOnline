using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class YmnosCustomRule : RuleExecutable, IYmnosStructureRuleElement, ICustomInterpreted
    {
        public YmnosCustomRule(string name) : base(name) { }

        public YmnosGroup Element { get; set; }

        public YmnosRuleKind Kind { get; set; }

        public YmnosStructure GetStructure(RuleHandlerSettings settings)
        {
            if (!IsValid)
            {
                return null;
            }

            YmnosStructure result = new YmnosStructure();

            switch (Kind)
            {
                case YmnosRuleKind.Ymnos:
                    result.Groups.Add(Element);
                    break;
                case YmnosRuleKind.Doxastichon:
                    result.Doxastichon = Element;
                    break;
                case YmnosRuleKind.Theotokion:
                    result.Theotokion.Add(Element);
                    break;
            }

            return result;
        }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<YmnosCustomRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Element == null)
            {
                AddBrokenConstraint(YmnosCustomRuleBusinessConstraint.ElementRequired);
            }
        }
    }

    public class YmnosCustomRuleBusinessConstraint
    {
        public static readonly BusinessConstraint ElementRequired = new BusinessConstraint("Текст песнопения должен быть определен.");
    }
}
