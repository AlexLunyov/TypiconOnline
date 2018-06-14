using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для стихир на Хвалитех
    /// </summary>
    public class AinoiRule : KekragariaRule
    {
        public AinoiRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, string name) : base(viewModelFactory, name) { }

    }
}
