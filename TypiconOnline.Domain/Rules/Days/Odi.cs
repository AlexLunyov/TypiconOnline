using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнь канона
    /// </summary>
    [Serializable]
    public class Odi : DayElementBase
    {
        public Odi() { }

        public Odi(XmlNode node) 
        {
            //номер песни
            XmlAttribute numberAttr = node.Attributes[RuleConstants.OdiNumberAttrName];
            if (numberAttr != null)
            {
                int result = default(int);
                int.TryParse(numberAttr.Value, out result);
                Number = result;
            }

            //ирмос
            XmlNode elemNode = node.SelectSingleNode(RuleConstants.OdiIrmosNode);
            if (elemNode != null)
            {
                Irmos = new Ymnos(elemNode);
            }

            //тропари

            XmlNodeList tropNodes = node.SelectNodes(RuleConstants.OdiTroparionName);
            if (tropNodes != null)
            {
                TroparionCollection = new List<Ymnos>();

                foreach (XmlNode tropNode in tropNodes)
                {
                    TroparionCollection.Add(new Ymnos(tropNode));
                }
            }

            //катавасия
            elemNode = node.SelectSingleNode(RuleConstants.OdiKatavasiaNode);
            if (elemNode != null)
            {
                Katavasia = new ItemText(elemNode.OuterXml);
            }
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(RuleConstants.OdiNumberAttrName)]
        public int Number { get; set; }

        /// <summary>
        /// Ирмос
        /// </summary>
        [XmlElement(RuleConstants.OdiIrmosNode)]
        public Ymnos Irmos { get; set; }
        /// <summary>
        /// Тропари песни канона
        /// </summary>
        [XmlElement(RuleConstants.OdiTroparionName)]
        public List<Ymnos> TroparionCollection { get; set; }
        /// <summary>
        /// Ирмос
        /// </summary>
        [XmlElement(RuleConstants.OdiKatavasiaNode)]
        public ItemText Katavasia { get; set; }

        #endregion

        protected override void Validate()
        {
            if ((Number < 1) || (Number > 9))
            {
                //номер песни должен иметь значения с 1 до 9
                AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, RuleConstants.KanonasOdiNode);
            }

            if (Irmos == null)
            {
                AddBrokenConstraint(OdiBusinessConstraint.IrmosRequired);
            }
            else if (!Irmos.IsValid)
            {
                AppendAllBrokenConstraints(Irmos, RuleConstants.OdiIrmosNode);
            }

            foreach (Ymnos trop in TroparionCollection)
            {
                if (!trop.IsValid)
                {
                    AppendAllBrokenConstraints(trop, RuleConstants.OdiTroparionName);
                }
            }

            if (Katavasia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Katavasia, RuleConstants.OdiKatavasiaNode);
            }
        }
    }
}
