using System;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    [Serializable]
    public class ApostlesPart : DayElementBase
    {
        public ApostlesPart() { }

        /// <summary>
        /// Ноемр зачала
        /// </summary>
        [XmlAttribute(ElementConstants.EvangelionPartNumberAttr)]
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
