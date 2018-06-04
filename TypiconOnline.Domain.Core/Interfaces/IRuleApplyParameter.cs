using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Core.Interfaces
{
    public interface IRuleApplyParameter : IRuleCustomParameter
    {
        //bool CorrespondsTo(RuleElement element);
        void Apply(RuleElement element);
    }
}