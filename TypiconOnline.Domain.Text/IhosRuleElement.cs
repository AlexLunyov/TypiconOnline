using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Абстрактный класс, содержащий описание гласа
    /// </summary>
    [Serializable]
    public abstract class IhosRuleElement : RuleElement
    {
        public IhosRuleElement() { }

        public IhosRuleElement(IhosRuleElement source)
        {
            if (source == null) throw new ArgumentNullException("IhosRuleElement");

            ElementName = string.Copy(source.ElementName);

            Ihos = source.Ihos;
        }

        public IhosRuleElement(XmlNode node) : base(node)
        {
            //глас
            XmlAttribute ihosAttr = node.Attributes[XmlConstants.YmnosIhosAttrName];
            if (ihosAttr != null)
            {
                int result = default(int);
                int.TryParse(ihosAttr.Value, out result);
                Ihos = result;
            }
        }

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(XmlConstants.YmnosIhosAttrName)]
        public int Ihos { get; set; }

        protected override void Validate()
        {
            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, XmlConstants.ProkeimenonNode);
            }
        }
    }
}
