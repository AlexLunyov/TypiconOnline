using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IScheduleCustomParameter
    {
        //bool CorrespondsTo(RuleElement element);
        void Apply(RuleElement element);
    }
}