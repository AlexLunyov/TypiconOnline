using System;
using System.Collections.Generic;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание канона (на утрене, повечерии, полунощнице, молебне и т.д.)
    /// </summary>
    public class Kanonas : IhosRuleElement
    {
        public Kanonas(XmlNode node) : base(node)
        {
            XmlNode elemNode = node.SelectSingleNode(RuleConstants.KanonasAcrosticNode);
            if (elemNode != null)
            {
                Acrostic = new ItemText(elemNode.OuterXml);
            }

            Odes = new List<Odi>();

            XmlNodeList odiNodes = node.SelectNodes(RuleConstants.KanonasOdiNode);
            if (odiNodes != null)
            {
                foreach (XmlNode odiNode in odiNodes)
                {
                    Odes.Add(new Odi(odiNode));
                }
            }

            elemNode = node.SelectSingleNode(RuleConstants.KanonasSedalenNode);
            if (elemNode != null)
            {
                Sedalen = new YmnosGroup(elemNode);
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
        /// Акростих, краегранесие
        /// </summary>
        public ItemText Acrostic { get; set; }
        /// <summary>
        /// Песни канона
        /// </summary>
        public List<Odi> Odes { get; set; }
        /// <summary>
        /// седален по 3-ой песне
        /// </summary>
        public YmnosGroup Sedalen { get; set; }
        /// <summary>
        /// кондак по 6-ой песне
        /// </summary>
        public Kontakion Kontakion { get; set; }
        /// <summary>
        /// Эксапостиларий по 9-й песне
        /// </summary>
        public Exapostilarion Exapostilarion { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            base.Validate();

            if (Acrostic?.IsValid == false)
            {
                AppendAllBrokenConstraints(Acrostic, ElementName + "." + RuleConstants.KanonasAcrosticNode);
            }

            foreach (Odi odi in Odes)
            {
                if (!odi.IsValid)
                {
                    AppendAllBrokenConstraints(odi, ElementName + "." + RuleConstants.KanonasOdiNode);
                }
            }

            if (Sedalen?.IsValid == false)
            {
                AppendAllBrokenConstraints(Sedalen, ElementName + "." + RuleConstants.KanonasSedalenNode);
            }

            if (Kontakion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kontakion, ElementName + "." + RuleConstants.KanonasKontakionNode);
            }

            if (Exapostilarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Exapostilarion, ElementName + "." + RuleConstants.KanonasExapostilarionNode);
            }
        }
    }
}