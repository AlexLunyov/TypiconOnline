using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание службы малой вечерни
    /// </summary>
    [Serializable]
    [XmlRoot(XmlConstants.MikrosEsperinosNode)]
    public class MikrosEsperinos : DayElementBase
    {
        public MikrosEsperinos() { }

        #region Properties
        /// <summary>
        /// Господи воззвах
        /// </summary>
        [XmlElement(XmlConstants.KekragariaNode)]
        public YmnosStructure Kekragaria { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        [XmlElement(XmlConstants.ApostichaNode)]
        public YmnosStructure Aposticha { get; set; }
        /// <summary>
        /// Отпустительный тропарь
        /// </summary>
        [XmlElement(XmlConstants.TroparionNode)]
        public YmnosStructure Troparion { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Kekragaria?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kekragaria, XmlConstants.MikrosEsperinosNode + "." + XmlConstants.KekragariaNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, XmlConstants.MikrosEsperinosNode + "." + XmlConstants.ApostichaNode);
            }

            if (Troparion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Troparion, XmlConstants.MikrosEsperinosNode + "." + XmlConstants.TroparionNode);
            }
        }
    }
}
