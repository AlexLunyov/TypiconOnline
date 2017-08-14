using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rendering
{
    public class RenderTextHolder : RenderElement
    {
        public TextHolderKind Kind;
        public List<string> Paragraphs = new List<string>();

        public RenderTextHolder(TextHolder textHolder, string language)
        {
            if (textHolder == null)
            {
                throw new ArgumentNullException("TextHolder");
            }

            textHolder.ThrowExceptionIfInvalid();

            foreach(ItemText itemText in textHolder.Paragraphs)
            {
                Paragraphs.Add(itemText.Text[language]);
            }
        }
    }
}
