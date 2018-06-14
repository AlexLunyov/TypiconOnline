using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Класс для описания правил седльна по кафизме
    /// </summary>
    public class SedalenRule : YmnosStructureRule
    {
        public SedalenRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, string name) 
            : base(viewModelFactory, name) { }
    }
}
