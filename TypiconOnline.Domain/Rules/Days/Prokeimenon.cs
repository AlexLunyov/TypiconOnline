using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание прокимна
    /// </summary>
    [Serializable]
    public class Prokeimenon : ItemTextCollection
    {
        public Prokeimenon() : base() { }

        public Prokeimenon(XmlNode node) : base(node)
        {
            //глас
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            if (ihosAttr != null)
            {
                int result = default(int);
                int.TryParse(ihosAttr.Value, out result);
                Ihos = result;
            }
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.YmnosIhosAttrName)]
        public int Ihos { get; set; }
        [XmlAttribute(RuleConstants.ProkeimenonKindAttr)]//(AttributeName = RuleConstants.ProkeimenonKindAttr, Type = typeof(ProkiemenonKind))]
        public ProkiemenonKind Kind { get; set; }

        #endregion

        protected override XmlDocument ComposeXml()
        {
            XmlDocument doc = base.ComposeXml();

            XmlAttribute attr = doc.CreateAttribute(RuleConstants.YmnosIhosAttrName);
            attr.Value = Ihos.ToString();
            doc.DocumentElement.Attributes.Append(attr);

            return doc;
        }

        protected override void Validate()
        {
            base.Validate();

            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, RuleConstants.ProkeimenonNode);
            }
        }
    }
}
