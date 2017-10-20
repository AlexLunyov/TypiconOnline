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

namespace TypiconOnline.Domain.ViewModels
{
    public class TextHolderViewModel : ElementViewModel
    {
        public TextHolderKind Kind { get; set; }
        public IEnumerable<string> Paragraphs { get; set; } 
            
        public TextHolderViewModel()
        {
            Paragraphs = new List<string>();
        }

        public TextHolderViewModel(TextHolder textHolder, IRuleHandler handler)
        {
            if (textHolder == null) throw new ArgumentNullException("textHolder");
            if (handler == null) throw new ArgumentNullException("handler");

            textHolder.ThrowExceptionIfInvalid();

            Kind = textHolder.Kind.Value;

            Paragraphs = textHolder.Paragraphs.Select(c => c[handler.Settings.Language]).ToArray();

            //foreach (ItemText itemText in textHolder.Paragraphs)
            //{
            //    Paragraphs.Add(itemText.Text[handler.Settings.Language]);
            //}
        }
    }
}
