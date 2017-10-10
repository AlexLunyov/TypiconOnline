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
    [XmlRoot(ElementName = RuleConstants.DayElementNode)]
    public class DayContainer : DayElementBase
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
        /// Служба шестого часа в Триоди
        /// </summary>
        [XmlElement(RuleConstants.SixHourNode)]
        public SixHour SixHour { get; set; }
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

        
        /// <summary>
        /// Возвращает тексты из определенного места в службе
        /// </summary>
        /// <param name="place">Место</param>
        /// <param name="count">Количество</param>
        /// <param name="startFrom">С какого по номеру песнопения начинать выборку</param>
        /// <returns></returns>
        public YmnosStructure GetYmnosStructure(PlaceYmnosSource place, int count, int startFrom)
        {
            ThrowExceptionIfInvalid();

            YmnosStructure stichera = null;

            switch (place)
            {
                //kekragaria
                case PlaceYmnosSource.kekragaria:
                    stichera = Esperinos?.Kekragaria?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.kekragaria_doxastichon:
                    if (Esperinos?.Kekragaria?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Esperinos.Kekragaria.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.kekragaria_theotokion:
                    if (Esperinos?.Kekragaria?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Esperinos.Kekragaria.Theotokion };
                    }
                    break;
                //liti
                case PlaceYmnosSource.liti:
                    stichera = Esperinos?.Liti?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.liti_doxastichon:
                    if (Esperinos?.Liti?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Esperinos.Liti.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.liti_theotokion:
                    if (Esperinos?.Liti?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Esperinos.Liti.Theotokion };
                    }
                    break;
                //aposticha_esperinos
                case PlaceYmnosSource.aposticha_esperinos:
                    stichera = Esperinos?.Aposticha?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_esperinos_doxastichon:
                    if (Esperinos?.Aposticha?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Esperinos.Aposticha.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.aposticha_esperinos_theotokion:
                    if (Esperinos?.Aposticha?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Esperinos.Aposticha.Theotokion };
                    }
                    break;
                //ainoi
                case PlaceYmnosSource.ainoi:
                    stichera = Orthros?.Ainoi?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.ainoi_doxastichon:
                    if (Orthros?.Ainoi?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Orthros.Ainoi.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.ainoi_theotokion:
                    if (Orthros?.Ainoi?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Orthros.Ainoi.Theotokion };
                    }
                    break;
                //aposticha_orthros
                case PlaceYmnosSource.aposticha_orthros:
                    stichera = Orthros?.Aposticha?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_orthros_doxastichon:
                    if (Orthros?.Aposticha?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Orthros.Aposticha.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.aposticha_orthros_theotokion:
                    if (Orthros?.Aposticha?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Orthros.Aposticha.Theotokion };
                    }
                    break;
            }

            return stichera;
        }
    }
}
