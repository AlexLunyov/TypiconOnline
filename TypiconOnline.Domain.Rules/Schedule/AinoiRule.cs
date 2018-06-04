using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.ViewModels;

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
