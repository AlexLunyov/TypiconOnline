using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Стих из чтения Священного Писания
    /// </summary>
    [Serializable]
    public class BookStihos : ItemText, IPsalterElement
    {
        [XmlAttribute(ElementConstants.ReadingChapterNumberAttr)]
        public int ChapterNumber { get; set; }
        [XmlAttribute(ElementConstants.ReadingStihosNumberAttr)]
        public int StihosNumber { get; set; }

        //public override void ReadXml(XmlReader reader)
        //{
        //    reader.MoveToElement();

        //    ChapterNumber = TryParse(reader.GetAttribute(ElementConstants.ReadingChapterNumberAttr));
        //    StihosNumber = TryParse(reader.GetAttribute(ElementConstants.ReadingStihosNumberAttr));
            
        //    base.ReadXml(reader);

        //    int? TryParse(string str)
        //    {
        //        if (int.TryParse(str, out int i))
        //        {
        //            return i;
        //        }
        //        return null;
        //    }
        //}

        //public override void WriteXml(XmlWriter writer)
        //{
        //    Write(ChapterNumber, ElementConstants.ReadingChapterNumberAttr);
        //    Write(StihosNumber, ElementConstants.ReadingStihosNumberAttr);

        //    base.WriteXml(writer);

        //    void Write(int? i, string str)
        //    {
        //        if (i != null)
        //        {
        //            writer.WriteAttributeString(str, string.Empty, i.ToString());
        //        }
        //    }
        //}
    }
}
