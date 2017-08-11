using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание службы малой вечерни
    /// </summary>
    public class MikrosEsperinos : RuleElement
    {
        public MikrosEsperinos(XmlNode node) : base(node)
        {
            //kekragaria
            XmlNode elementNode = node.SelectSingleNode(RuleConstants.KekragariaNode);
            if (elementNode != null)
            {
                Kekragaria = new YmnosStructure(elementNode);
            }

            //Aposticha
            elementNode = node.SelectSingleNode(RuleConstants.ApostichaNode);
            if (elementNode != null)
            {
                Aposticha = new YmnosStructure(elementNode);
            }

            //Troparion
            elementNode = node.SelectSingleNode(RuleConstants.TroparionNode);
            if (elementNode != null)
            {
                Troparion = new YmnosStructure(elementNode);
            }

        }

        #region Properties
        /// <summary>
        /// Господи воззвах
        /// </summary>
        public YmnosStructure Kekragaria { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        public YmnosStructure Aposticha { get; set; }
        /// <summary>
        /// Отпустительный тропарь
        /// </summary>
        public YmnosStructure Troparion { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            if (Kekragaria?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kekragaria, ElementName + "." + RuleConstants.KekragariaNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, ElementName + "." + RuleConstants.ApostichaNode);
            }

            if (Troparion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Troparion, ElementName + "." + RuleConstants.TroparionNode);
            }
        }
    }
}
