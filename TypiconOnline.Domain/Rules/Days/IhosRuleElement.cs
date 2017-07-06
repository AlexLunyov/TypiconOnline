using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Абстрактный класс, содержащий описание гласа
    /// </summary>
    public abstract class IhosRuleElement : RuleElement
    {
        public IhosRuleElement(XmlNode node) : base(node)
        {
            //глас
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            Ihos = (ihosAttr != null) ? new ItemInt(ihosAttr.Value) : new ItemInt();
        }

        /// <summary>
        /// Глас
        /// </summary>
        public ItemInt Ihos { get; set; }

        protected override void Validate()
        {
            if (!Ihos.IsValid)
            {
                AppendAllBrokenConstraints(Ihos, ElementName + "." + RuleConstants.YmnosIhosAttrName);
            }
            else if (!Ihos.IsEmpty)
            {
                //глас должен иметь значения с 1 до 8
                if ((Ihos.Value < 1) || (Ihos.Value > 8))
                {
                    AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, ElementName);
                }
            }
        }
    }
}
