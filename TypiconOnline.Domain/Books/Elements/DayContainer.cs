using System;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Класс - хранилище текста службы дня
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = ElementConstants.DayElementNode)]
    public class DayContainer : DayElementBase
    {
        public DayContainer() { }

        #region Properties

        /// <summary>
        /// Наименование праздника.
        /// Например, "Великомученика Никиты."
        /// </summary>
        [XmlElement(ElementConstants.DayElementNameNode)]
        public ItemText Name { get; set; }
        /// <summary>
        /// Описание службы малой вечерни
        /// </summary>
        [XmlElement(ElementConstants.MikrosEsperinosNode)]
        public MikrosEsperinos MikrosEsperinos { get; set; }
        /// <summary>
        /// Описание службы вечерни
        /// </summary>
        [XmlElement(ElementConstants.EsperinosNode)]
        public Esperinos Esperinos { get; set; }
        /// <summary>
        /// Описание службы утрени
        /// </summary>
        [XmlElement(ElementConstants.OrthrosNode)]
        public Orthros Orthros { get; set; }
        /// <summary>
        /// Служба шестого часа в Триоди
        /// </summary>
        [XmlElement(ElementConstants.SixHourNode)]
        public SixHour SixHour { get; set; }
        /// <summary>
        /// Описание Литургийных чтений
        /// </summary>
        [XmlElement(ElementConstants.LeitourgiaNode)]
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
                AppendAllBrokenConstraints(Name, ElementConstants.DayElementNameNode);
            }

            if (MikrosEsperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(MikrosEsperinos, ElementConstants.MikrosEsperinosNode);
            }

            if (Esperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Esperinos, ElementConstants.EsperinosNode);
            }

            if (Orthros?.IsValid == false)
            {
                AppendAllBrokenConstraints(Orthros, ElementConstants.OrthrosNode);
            }

            if (Leitourgia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Leitourgia, ElementConstants.LeitourgiaNode);
            }

            //тропарь должен быть определен
            //bool troparionExists = MikrosEsperinos?.Troparion != null || Esperinos?.Troparion != null;
            //if (!troparionExists)
            //{
            //    AddBrokenConstraint(DayContainerBusinessConstraint.TroparionRequired);
            //}
        }
    }
}
