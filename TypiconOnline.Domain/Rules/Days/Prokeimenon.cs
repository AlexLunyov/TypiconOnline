using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание прокимна
    /// </summary>
    public class Prokeimenon : ItemTextCollection
    {
        public Prokeimenon(XmlNode node) : base(node)
        {
            //глас
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            Ihos = (ihosAttr != null) ? new ItemInt(ihosAttr.Value) : new ItemInt();
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        public ItemInt Ihos { get; set; }

        #endregion

        protected override XmlDocument ComposeXml()
        {
            XmlDocument doc = base.ComposeXml();

            XmlAttribute attr = doc.CreateAttribute(RuleConstants.YmnosIhosAttrName);
            attr.Value = Ihos.Value.ToString();
            doc.DocumentElement.Attributes.Append(attr);

            return doc;
        }

        protected override void Validate()
        {
            base.Validate();

            if (!Ihos.IsValid)
            {
                AppendAllBrokenConstraints(Ihos, RuleConstants.ProkeimenonNode + "." + RuleConstants.YmnosIhosAttrName);
            }
            else
            {
                //глас должен иметь значения с 1 до 8
                if ((Ihos.Value < 1) || (Ihos.Value > 8))
                {
                    AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, RuleConstants.ProkeimenonNode);
                }
            }
        }
    }
}
