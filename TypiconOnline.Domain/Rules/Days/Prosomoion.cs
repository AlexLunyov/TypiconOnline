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
            bool result = base.Equals(item);

            if (item is Prosomoion)
            {
                result = result && Self.Equals((item as Prosomoion).Self);
            }

            return result;
        }
    }
}
