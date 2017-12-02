using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Элементарная частичка выходной формы последовательности богослужений
    /// </summary>
    public class ViewModelItem
    {
        public string KindStringValue { get; set; }
        public TextHolderKind Kind { get; set; }
        public IEnumerable<string> Paragraphs { get; set; }
    }
}
