namespace TypiconOnline.Domain.Rules.Handlers
{
    public abstract class RuleHandlerBase<T> : RuleHandlerBase where T : class
    {
        public abstract T GetResult();
    }
}
