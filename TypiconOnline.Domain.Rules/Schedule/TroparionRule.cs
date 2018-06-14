using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для стихир на стиховне и литии
    /// </summary>
    public class TroparionRule : YmnosStructureRule
    {
        public TroparionRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, string name) 
            : base(viewModelFactory, name) { }

    }
}
