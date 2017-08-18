using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    interface IRuleFactory
    {
        RuleElement CreateElement(string description);
    }
}
