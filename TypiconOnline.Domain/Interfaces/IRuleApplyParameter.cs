namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleApplyParameter : IRuleCustomParameter
    {
        //bool CorrespondsTo(RuleElement element);
        void Apply(IRuleElement element);
    }
}