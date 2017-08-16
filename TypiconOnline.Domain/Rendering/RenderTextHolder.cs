using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rendering
{
    public class RenderTextHolder : RenderElement
    {
        public TextHolderKind Kind { get; set; }
        public IEnumerable<string> Paragraphs { get; set; } 
            
        public RenderTextHolder()
        {
            Paragraphs = new List<string>();
        }

        public RenderTextHolder(TextHolder textHolder, RuleHandlerBase handler)
        {
            if (textHolder == null) throw new ArgumentNullException("textHolder");
            if (handler == null) throw new ArgumentNullException("handler");

            textHolder.ThrowExceptionIfInvalid();

            Kind = textHolder.Kind.Value;

            Paragraphs = textHolder.Paragraphs.Select(c => c.Text[handler.Settings.Language]);

            //foreach (ItemText itemText in textHolder.Paragraphs)
            //{
            //    Paragraphs.Add(itemText.Text[handler.Settings.Language]);
            //}
        }
    }
}
