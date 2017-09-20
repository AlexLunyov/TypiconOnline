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
    /// Класс - хранилище текста службы дня
    /// </summary>
    [Serializable]
    public class DayContainer : ValueObjectBase
    {
        public DayContainer() { }

        public DayContainer(XmlNode node) 
        {
            XmlNode nameNode = node.SelectSingleNode(RuleConstants.DayElementNameNode);
            if (nameNode != null)
            {
                Name = new ItemText(nameNode.OuterXml);
            }

            //ищем mikrosEsperinos
            XmlNode mikrosEsperinosNode = node.SelectSingleNode(RuleConstants.MikrosEsperinosNode);
            if (mikrosEsperinosNode != null)
            {
                MikrosEsperinos = new MikrosEsperinos(mikrosEsperinosNode);
            }

            //ищем esperinos
            XmlNode esperinosNode = node.SelectSingleNode(RuleConstants.EsperinosNode);
            if (esperinosNode != null)
            {
                Esperinos = new Esperinos(esperinosNode);
            }

            XmlNode orthrosNode = node.SelectSingleNode(RuleConstants.OrthrosNode);
            if (orthrosNode != null)
            {
                Orthros = new Orthros(orthrosNode);
            }

            XmlNode leitourgiaNode = node.SelectSingleNode(RuleConstants.LeitourgiaNode);
            if (leitourgiaNode != null)
            {
                Leitourgia = new Leitourgia(leitourgiaNode);
            }
        }

        #region Properties

        /// <summary>
        /// Наименование праздника.
        /// Например, "Великомученика Никиты."
        /// </summary>
        [XmlElement(RuleConstants.DayElementNameNode)]
        public ItemText Name { get; set; }
        /// <summary>
        /// Описание службы малой вечерни
        /// </summary>
        [XmlElement(RuleConstants.MikrosEsperinosNode)]
        public MikrosEsperinos MikrosEsperinos { get; set; }
        /// <summary>
        /// Описание службы вечерни
        /// </summary>
        [XmlElement(RuleConstants.EsperinosNode)]
        public Esperinos Esperinos { get; set; }
        /// <summary>
        /// Описание службы утрени
        /// </summary>
        [XmlElement(RuleConstants.OrthrosNode)]
        public Orthros Orthros { get; set; }
        /// <summary>
        /// Описание Литургийных чтений
        /// </summary>
        [XmlElement(RuleConstants.LeitourgiaNode)]
        public Leitourgia Leitourgia { get; set; }

        #endregion

        protected override void Validate()
        {
            /*if (Name == null)
            {
                AddBrokenConstraint(DayElementBusinessConstraint.NameRequired, ElementName);
            }
            else */if (Name?.IsValid == false)
            {
                AppendAllBrokenConstraints(Name, RuleConstants.DayElementNameNode);
            }

            if (MikrosEsperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(MikrosEsperinos, RuleConstants.MikrosEsperinosNode);
            }

            if (Esperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Esperinos, RuleConstants.EsperinosNode);
            }

            if (Orthros?.IsValid == false)
            {
                AppendAllBrokenConstraints(Orthros, RuleConstants.OrthrosNode);
            }

            if (Leitourgia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Leitourgia, RuleConstants.LeitourgiaNode);
            }
        }
    }
}
