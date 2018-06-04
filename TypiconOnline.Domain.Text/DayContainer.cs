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
    [XmlRoot(ElementName = XmlConstants.DayElementNode)]
    public class DayContainer : DayElementBase
    {
        public DayContainer() { }

        #region Properties

        /// <summary>
        /// Наименование праздника.
        /// Например, "Великомученика Никиты."
        /// </summary>
        [XmlElement(XmlConstants.DayElementNameNode)]
        public ItemText Name { get; set; }
        /// <summary>
        /// Описание службы малой вечерни
        /// </summary>
        [XmlElement(XmlConstants.MikrosEsperinosNode)]
        public MikrosEsperinos MikrosEsperinos { get; set; }
        /// <summary>
        /// Описание службы вечерни
        /// </summary>
        [XmlElement(XmlConstants.EsperinosNode)]
        public Esperinos Esperinos { get; set; }
        /// <summary>
        /// Описание службы утрени
        /// </summary>
        [XmlElement(XmlConstants.OrthrosNode)]
        public Orthros Orthros { get; set; }
        /// <summary>
        /// Служба шестого часа в Триоди
        /// </summary>
        [XmlElement(XmlConstants.SixHourNode)]
        public SixHour SixHour { get; set; }
        /// <summary>
        /// Описание Литургийных чтений
        /// </summary>
        [XmlElement(XmlConstants.LeitourgiaNode)]
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
                AppendAllBrokenConstraints(Name, XmlConstants.DayElementNameNode);
            }

            if (MikrosEsperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(MikrosEsperinos, XmlConstants.MikrosEsperinosNode);
            }

            if (Esperinos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Esperinos, XmlConstants.EsperinosNode);
            }

            if (Orthros?.IsValid == false)
            {
                AppendAllBrokenConstraints(Orthros, XmlConstants.OrthrosNode);
            }

            if (Leitourgia?.IsValid == false)
            {
                AppendAllBrokenConstraints(Leitourgia, XmlConstants.LeitourgiaNode);
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
