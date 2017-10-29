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
    public class ApostlesPart : DayElementBase
    {
        public ApostlesPart() { }

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
