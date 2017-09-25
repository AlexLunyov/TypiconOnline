using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    [Serializable]
    public class ApostlesPart : ValueObjectBase
    {
        public ApostlesPart() { }

        public ApostlesPart(XmlNode node)
        {
            //номер
            XmlAttribute numberAttr = node.Attributes[RuleConstants.EvangelionPartNumberAttr];
            if (numberAttr != null)
            {
                int result = default(int);
                int.TryParse(numberAttr.Value, out result);
                Number = result;
            }
        }

        /// <summary>
        /// Ноемр зачала
        /// </summary>
        [XmlAttribute(RuleConstants.EvangelionPartNumberAttr)]
        public int Number { get; set; }

        protected override void Validate()
        {
            if (Number < 0)
            {
                AddBrokenConstraint(EvangelionPartBusinessConstraint.InvalidNumber);
            }
        }
    }
}
