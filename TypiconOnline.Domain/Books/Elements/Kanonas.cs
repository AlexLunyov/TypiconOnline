using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание канона (на утрене, повечерии, полунощнице, молебне и т.д.)
    /// </summary>
    [Serializable]
    [XmlRoot(ElementConstants.KanonasNode)]
    public class Kanonas : DayElementBase, IContainingIhos
    {
        public Kanonas() { }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(ElementConstants.IhosAttrName)]
        public int Ihos { get; set; }

        /// <summary>
        /// Наименование канона
        /// </summary>
        [XmlElement(ElementConstants.KanonasNameNode)]
        public ItemText Name { get; set; }

        /// <summary>
        /// Аннотация
        /// </summary>
        [XmlElement(ElementConstants.KanonasAnnotationNode)]
        public ItemText Annotation { get; set; }

        /// <summary>
        /// Акростих, краегранесие
        /// </summary>
        [XmlElement(ElementConstants.KanonasAcrosticNode)]
        public ItemText Acrostic { get; set; }

        /// <summary>
        /// Припев канона. Например:
        /// Пресвятая Богородице, спаси нас
        /// </summary>
        [XmlElement(ElementConstants.KanonasStihosNode)]
        public ItemText Stihos { get; set; }
        /// <summary>
        /// Песни канона
        /// </summary>
        [XmlArray(ElementConstants.KanonasOdesNode)]
        [XmlArrayItem(ElementName = ElementConstants.KanonasOdiNode, Type = typeof(Odi))]
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
            //    AppendAllBrokenConstraints(Name, ElementConstants.KanonasNameNode);
            //}

            if (Acrostic?.IsValid == false)
            {
                AppendAllBrokenConstraints(Acrostic, ElementConstants.KanonasAcrosticNode);
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
                        AppendAllBrokenConstraints(odi, ElementConstants.KanonasOdiNode);
                    }
                }
            }

            
        }
    }
}