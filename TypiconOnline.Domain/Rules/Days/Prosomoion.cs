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
        public Prosomoion(Prosomoion source)
        {
            if (source == null) throw new ArgumentNullException("Prosomoion");

            Self = source.Self;
            Build(source.StringExpression);
        }

        public Prosomoion() : base()
        {
        }

        public Prosomoion(XmlNode node)
        {
            //самоподобен?
            XmlAttribute selfAttr = node.Attributes[RuleConstants.ProsomoionSelfAttr];

            if (selfAttr != null)
            {
                bool result = false;
                bool.TryParse(selfAttr.Value, out result);
                Self = result;
            }

            Build(node.OuterXml);
        }

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

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToAttribute(RuleConstants.ProsomoionSelfAttr))
            {
                string val = reader.Value;

                bool result = false;
                bool.TryParse(val, out result);
                Self = result;

                reader.MoveToElement();
            }

            base.ReadXml(reader);
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (Self)
            {
                writer.WriteAttributeString(RuleConstants.ProsomoionSelfAttr, Self.ToString());
            }

            base.WriteXml(writer);
        }

        #endregion
    }
}
