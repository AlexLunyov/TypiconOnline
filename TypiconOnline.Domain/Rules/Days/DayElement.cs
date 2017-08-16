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
    /// Текст службы дня
    /// </summary>
    public class DayElement : RuleElement
    {
        public DayElement(XmlNode node) : base(node)
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
        public ItemText Name { get; set; }
        /// <summary>
        /// Описание службы малой вечерни
        /// </summary>
        public MikrosEsperinos MikrosEsperinos { get; set; }
        /// <summary>
        /// Описание службы вечерни
        /// </summary>
        public Esperinos Esperinos { get; set; }
        /// <summary>
        /// Описание службы утрени
        /// </summary>
        public Orthros Orthros { get; set; }
        /// <summary>
        /// Описание Литургийных чтений
        /// </summary>
        public Leitourgia Leitourgia { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            /*if (Name == null)
            {
                AddBrokenConstraint(DayElementBusinessConstraint.NameRequired, ElementName);
            }
            else */if (Name?.IsValid == false)
            {
                AppendAllBrokenConstraints(Name, ElementName + "." + RuleConstants.DayElementNameNode);
            }

            if (MikrosEsperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(MikrosEsperinos, ElementName + "." + RuleConstants.MikrosEsperinosNode);
            }

            if (Esperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Esperinos, ElementName + "." + RuleConstants.EsperinosNode);
            }

            if (Orthros?.IsValid == false)
            {
                AppendAllBrokenConstraints(Orthros, ElementName + "." + RuleConstants.OrthrosNode);
            }

            if (Leitourgia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Leitourgia, ElementName + "." + RuleConstants.LeitourgiaNode);
            }
        }
    }
}
