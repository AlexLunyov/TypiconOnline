using System;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание службы малой вечерни
    /// </summary>
    [Serializable]
    [XmlRoot(ElementConstants.MikrosEsperinosNode)]
    public class MikrosEsperinos : DayElementBase
    {
        public MikrosEsperinos() { }

        #region Properties
        /// <summary>
        /// Господи воззвах
        /// </summary>
        [XmlElement(ElementConstants.KekragariaNode)]
        public YmnosStructure Kekragaria { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        [XmlElement(ElementConstants.ApostichaNode)]
        public YmnosStructure Aposticha { get; set; }
        /// <summary>
        /// Отпустительный тропарь
        /// </summary>
        [XmlElement(ElementConstants.TroparionNode)]
        public YmnosStructure Troparion { get; set; }

        #endregion

        protected override void Validate()
        {
            if (Kekragaria?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kekragaria, ElementConstants.MikrosEsperinosNode + "." + ElementConstants.KekragariaNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, ElementConstants.MikrosEsperinosNode + "." + ElementConstants.ApostichaNode);
            }

            if (Troparion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Troparion, ElementConstants.MikrosEsperinosNode + "." + ElementConstants.TroparionNode);
            }
        }
    }
}
