using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Стих из чтения Священного Писания
    /// </summary>
    [Serializable]
    public class BookStihos : ItemText, IPsalterElement
    {
        [XmlAttribute(XmlConstants.ReadingChapterNumberAttr)]
        public int ChapterNumber { get; set; }
        [XmlAttribute(XmlConstants.ReadingStihosNumberAttr)]
        public int StihosNumber { get; set; }

        //public override void ReadXml(XmlReader reader)
        //{
        //    reader.MoveToElement();

        //    ChapterNumber = TryParse(reader.GetAttribute(XmlConstants.ReadingChapterNumberAttr));
        //    StihosNumber = TryParse(reader.GetAttribute(XmlConstants.ReadingStihosNumberAttr));
            
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
        //    Write(ChapterNumber, XmlConstants.ReadingChapterNumberAttr);
        //    Write(StihosNumber, XmlConstants.ReadingStihosNumberAttr);

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
