using System;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemTextUnit
    {
        public ItemTextUnit() { }

        public ItemTextUnit(ItemTextUnit source)
        {
            if (source == null) throw new ArgumentNullException("ItemTextUnit");

            Text = source.Text;
            Language = source.Language;
        }

        public ItemTextUnit(string language, string text)
        {
            Language = language;
            Text = text;
        }

        [XmlAttribute("language")]
        public string Language { get; set; }
        [XmlText()]
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
