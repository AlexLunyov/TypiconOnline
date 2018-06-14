using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание подобна
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = ElementConstants.ProsomoionNode)]
    public class Prosomoion : ItemText
    {
        public Prosomoion(Prosomoion source) : base(source)
        {
            Self = source.Self;
        }

        public Prosomoion() : base() { }

        /// <summary>
        /// Если true, то самоподобен
        /// </summary>
        [XmlAttribute(ElementConstants.ProsomoionSelfAttr)]
        public bool Self { get; set; }

        public bool Equals(Prosomoion item)
        {
            return base.Equals(item) && Self.Equals(item.Self);
        }

        #region IXmlSerializable

        //public override void ReadXml(XmlReader reader)
        //{
        //    if (reader.MoveToAttribute(ElementConstants.ProsomoionSelfAttr))
        //    {
        //        bool.TryParse(reader.Value, out bool result);

        //        Self = result;

        //        reader.MoveToElement();
        //    }

        //    base.ReadXml(reader);
        //}

        //public override void WriteXml(XmlWriter writer)
        //{
        //    if (Self)
        //    {
        //        writer.WriteAttributeString(ElementConstants.ProsomoionSelfAttr, Self.ToString());
        //    }

        //    base.WriteXml(writer);
        //}

        #endregion
    }
}
