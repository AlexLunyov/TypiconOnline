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
    public class Kanonas : DayElementBase, IContainingIhos
    {
        public Kanonas() { }

        public Kanonas(XmlNode node) 
        {
            //глас
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            if (ihosAttr != null)
            {
                int result = default(int);
                int.TryParse(ihosAttr.Value, out result);
                Ihos = result;
            }

            XmlNode elemNode = node.SelectSingleNode(RuleConstants.KanonasAcrosticNode);
            if (elemNode != null)
            {
                Acrostic = new ItemText(elemNode.OuterXml);
            }

            elemNode = node.SelectSingleNode(RuleConstants.KanonasStihosNode);
            if (elemNode != null)
            {
                Stihos = new ItemText(elemNode.OuterXml);
            }

            XmlNodeList odiNodes = node.SelectNodes(RuleConstants.KanonasOdiNode);
            if (odiNodes != null)
            {
                Odes = new List<Odi>();

                foreach (XmlNode odiNode in odiNodes)
                {
                    Odes.Add(new Odi(odiNode));
                }
            }

            elemNode = node.SelectSingleNode(RuleConstants.KanonasSedalenNode);
            if (elemNode != null)
            {
                Sedalen = new YmnosStructure(elemNode);
            }

            //elemNode = node.SelectSingleNode(RuleConstants.KanonasKontakionNode);
            //if (elemNode != null)
            //{
            //    Kontakion = new Kontakion(elemNode);
            //}

            //elemNode = node.SelectSingleNode(RuleConstants.KanonasExapostilarionNode);
            //if (elemNode != null)
            //{
            //    Exapostilarion = new Exapostilarion(elemNode);
            //}
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.IhosAttrName)]
        public int Ihos { get; set; }

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
        public List<Odi> Odes { get; set; }
        /// <summary>
        /// седален по 3-ой песне
        /// </summary>
        [XmlElement(RuleConstants.KanonasSedalenNode)]
        public YmnosStructure Sedalen { get; set; }
        /// <summary>
        /// кондак по 6-ой песне
        /// </summary>
        [XmlElement(RuleConstants.KanonasKontakionNode)]
        public Kontakion Kontakion { get; set; }
        /// <summary>
        /// Эксапостиларий по 9-й песне
        /// </summary>
        [XmlElement(RuleConstants.KanonasExapostilarionNode)]
        public Exapostilarion Exapostilarion { get; set; }

        

        #endregion

        protected override void Validate()
        {
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

            if (Sedalen?.IsValid == false)
            {
                AppendAllBrokenConstraints(Sedalen, RuleConstants.KanonasSedalenNode);
            }

            if (Kontakion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kontakion, RuleConstants.KanonasKontakionNode);
            }

            if (Exapostilarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Exapostilarion, RuleConstants.KanonasExapostilarionNode);
            }
        }
    }
}