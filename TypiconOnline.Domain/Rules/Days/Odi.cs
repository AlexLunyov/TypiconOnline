using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Песнь канона
    /// </summary>
    public class Odi : RuleElement
    {
        public Odi(XmlNode node) : base(node)
        {
            //номер песни
            XmlAttribute numberAttr = node.Attributes[RuleConstants.OdiNumberAttrName];
            Number = (numberAttr != null) ? new ItemInt(numberAttr.Value) : new ItemInt();

            //ирмос
            XmlNode elemNode = node.SelectSingleNode(RuleConstants.OdiIrmosNode);
            if (elemNode != null)
            {
                Irmos = new ItemText(elemNode.OuterXml);
            }

            //тропари
            TroparionCollection = new List<OdiTroparion>();

            XmlNodeList tropNodes = node.SelectNodes(RuleConstants.OdiTroparionName);
            if (tropNodes != null)
            {
                foreach (XmlNode tropNode in tropNodes)
                {
                    TroparionCollection.Add(new OdiTroparion(tropNode));
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
        public ItemInt Number { get; set; }

        /// <summary>
        /// Ирмос
        /// </summary>
        public ItemText Irmos { get; set; }
        /// <summary>
        /// Тропари песни канона
        /// </summary>
        public List<OdiTroparion> TroparionCollection { get; set; }
        /// <summary>
        /// Ирмос
        /// </summary>
        public ItemText Katavasia { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            //ничего
        }

        protected override void Validate()
        {
            if (!Number.IsValid)
            {
                AppendAllBrokenConstraints(Number, RuleConstants.KanonasOdiNode + "." + RuleConstants.OdiNumberAttrName);
            }
            else
            {
                //номер песни должен иметь значения с 1 до 9
                if ((Number.Value < 1) || (Number.Value > 9))
                {
                    AddBrokenConstraint(OdiBusinessConstraint.InvalidNumber, RuleConstants.KanonasOdiNode);
                }
            }

            if (Irmos == null || Irmos.IsEmpty == true)
            {
                AddBrokenConstraint(OdiBusinessConstraint.IrmosRequired, ElementName);
            }

            if (Irmos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Irmos, ElementName + "." + RuleConstants.OdiIrmosNode);
            }

            foreach (OdiTroparion trop in TroparionCollection)
            {
                if (!trop.IsValid)
                {
                    AppendAllBrokenConstraints(trop, ElementName + "." + RuleConstants.OdiTroparionName);
                }
            }

            if (Katavasia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Katavasia, ElementName + "." + RuleConstants.OdiKatavasiaNode);
            }
        }
    }
}
