using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание канона (на утрене, повечерии, полунощнице, молебне и т.д.)
    /// </summary>
    [Serializable]
    [XmlRoot(RuleConstants.KanonasNode)]
    public class Kanonas : DayElementBase, IContainingIhos
    {
        public Kanonas() { }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.IhosAttrName)]
        public int Ihos { get; set; }

        /// <summary>
        /// Наименование канона
        /// </summary>
        [XmlElement(RuleConstants.KanonasNameNode)]
        public ItemText Name { get; set; }

        /// <summary>
        /// Аннотация
        /// </summary>
        [XmlElement(RuleConstants.KanonasAnnotationNode)]
        public ItemText Annotation { get; set; }

        /// <summary>
        /// Акростих, краегранесие
        /// </summary>
        [XmlElement(RuleConstants.KanonasAcrosticNode)]
        public ItemText Acrostic { get; set; }

        /// <summary>
        /// Припев канона. Например:
        /// Пресвятая Богородице, спаси нас
        /// </summary>
        [XmlElement(RuleConstants.KanonasStihosNode)]
        public ItemText Stihos { get; set; }
        /// <summary>
        /// Песни канона
        /// </summary>
        [XmlArray(RuleConstants.KanonasOdesNode)]
        [XmlArrayItem(ElementName = RuleConstants.KanonasOdiNode, Type = typeof(Odi))]
        public List<Odi> Odes { get; set; } = new List<Odi>();
        

        

        #endregion

        protected override void Validate()
        {
            //if (Name == null)
            //{
            //    AddBrokenConstraint(KanonasBusinessConstraint.NameRequired);
            //}
            //else if (!Name.IsValid)
            //{
            //    AppendAllBrokenConstraints(Name, RuleConstants.KanonasNameNode);
            //}

            if (Acrostic?.IsValid == false)
            {
                AppendAllBrokenConstraints(Acrostic, RuleConstants.KanonasAcrosticNode);
            }

            if (Odes == null)
            {
                AddBrokenConstraint(KanonasBusinessConstraint.OdiRequired);
            }
            else
            {
                foreach (Odi odi in Odes)
                {
                    if (!odi.IsValid)
                    {
                        AppendAllBrokenConstraints(odi, RuleConstants.KanonasOdiNode);
                    }
                }
            }

            
        }
    }
}