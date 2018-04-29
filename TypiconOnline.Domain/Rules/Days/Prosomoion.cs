using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание подобна
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = RuleConstants.ProsomoionNode)]
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
        [XmlAttribute(RuleConstants.ProsomoionSelfAttr)]
        public bool Self { get; set; }

        public bool Equals(Prosomoion item)
        {
            return base.Equals(item) && Self.Equals(item.Self);
        }

        #region IXmlSerializable

        //public override void ReadXml(XmlReader reader)
        //{
        //    if (reader.MoveToAttribute(RuleConstants.ProsomoionSelfAttr))
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
        //        writer.WriteAttributeString(RuleConstants.ProsomoionSelfAttr, Self.ToString());
        //    }

        //    base.WriteXml(writer);
        //}

        #endregion
    }
}
