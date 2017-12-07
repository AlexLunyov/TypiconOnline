using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Стих из чтения Священного Писания
    /// </summary>
    public class BookStihos : ItemText, IPsalterElement
    {
        public int? ChapterNumber { get; set; }
        public int? StihosNumber { get; set; }

        public override void ReadXml(XmlReader reader)
        {
            reader.MoveToElement();

            ChapterNumber = TryParse(reader.GetAttribute(RuleConstants.ReadingChapterNumberAttr));
            StihosNumber = TryParse(reader.GetAttribute(RuleConstants.ReadingStihosNumberAttr));
            
            base.ReadXml(reader);

            int? TryParse(string str)
            {
                if (int.TryParse(str, out int i))
                {
                    return i;
                }
                return null;
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            Write(ChapterNumber, RuleConstants.ReadingChapterNumberAttr);
            Write(StihosNumber, RuleConstants.ReadingStihosNumberAttr);

            base.WriteXml(writer);

            void Write(int? i, string str)
            {
                if (i != null)
                {
                    writer.WriteAttributeString(str, string.Empty, i.ToString());
                }
            }
        }
    }
}
