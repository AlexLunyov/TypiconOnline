using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание кондака, содержащегося в каноне утрени
    /// </summary>
    [Serializable]
    public class Kontakion : DayElementBase, IContainingIhos
    {
        #region Properties
        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        [XmlElement(RuleConstants.ProsomoionNode)]
        public Prosomoion Prosomoion { get; set; }
        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(RuleConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }
        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.YmnosIhosAttrName)]
        public int Ihos { get; set; }

        [XmlElement(RuleConstants.YmnosNode)]
        public ItemText Ymnos { get; set; }

        [XmlElement(RuleConstants.IkosNode)]
        public ItemText Ikos { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Ymnos == null)
            {
                AddBrokenConstraint(KontakionBusinessConstraint.YmnosRequired);
            }

            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos);
            }
        }
    }
}
