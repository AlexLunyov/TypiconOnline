using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнопения, сгруппированные по гласам и подобнам
    /// </summary>
    public class YmnosIhosGroup : YmnosGroup, IContainingIhos
    {
        public YmnosIhosGroup(YmnosIhosGroup source) : base(source)
        {
            Ihos = source.Ihos;
        }

        public YmnosIhosGroup(XmlNode node) : base(node)
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
        public int Ihos { get; set; }

        protected override void Validate()
        {
            base.Validate();

            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos);
            }
        }

        public bool Equals(YmnosIhosGroup ymnosGroup)
        {
            if (ymnosGroup == null) throw new ArgumentNullException("YmnosIhosGroup.Equals");
            
            return (Ihos.Equals(ymnosGroup.Ihos) && Annotation.Equals(ymnosGroup.Annotation) && Prosomoion.Equals(ymnosGroup.Prosomoion));
        }
    }
}
