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

            //тропарь должен быть определен
            //bool troparionExists = MikrosEsperinos?.Troparion != null || Esperinos?.Troparion != null;
            //if (!troparionExists)
            //{
            //    AddBrokenConstraint(DayContainerBusinessConstraint.TroparionRequired);
            //}
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
                case PlaceYmnosSource.kekragaria_stavrostheotokion:
                    if (Esperinos?.Kekragaria?.Theotokion != null
                        && Esperinos.Kekragaria.Theotokion.Exists(c => c.Kind == YmnosGroupKind.Stavros))
                    {
                        //Оставляем только крестобородичен
                        stichera = new YmnosStructure() { Theotokion = Esperinos.Kekragaria.Theotokion };
                        stichera.Theotokion.RemoveAll(c => c.Kind == YmnosGroupKind.Undefined);
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
                //troparion
                case PlaceYmnosSource.troparion:
                    //Выбираем либо из Малой вечерни, либо с Вечерни тропарь
                    YmnosStructure y = (MikrosEsperinos?.Troparion != null) ? MikrosEsperinos.Troparion
                                        : Esperinos.Troparion;

                    stichera = new YmnosStructure(y);
                    break;

                //sedalen1
                case PlaceYmnosSource.sedalen1:
                    stichera = Orthros?.SedalenKathisma1?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.sedalen1_doxastichon:
                    if (Orthros?.SedalenKathisma1?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Orthros.SedalenKathisma1.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.sedalen1_theotokion:
                    if (Orthros?.SedalenKathisma1?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Orthros.SedalenKathisma1.Theotokion };
                    }
                    break;

                //sedalen2
                case PlaceYmnosSource.sedalen2:
                    stichera = Orthros?.SedalenKathisma2?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.sedalen2_doxastichon:
                    if (Orthros?.SedalenKathisma2?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Orthros.SedalenKathisma2.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.sedalen2_theotokion:
                    if (Orthros?.SedalenKathisma2?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Orthros.SedalenKathisma2.Theotokion };
                    }
                    break;

                //sedalen3
                case PlaceYmnosSource.sedalen3:
                    stichera = Orthros?.SedalenKathisma3?.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.sedalen3_doxastichon:
                    if (Orthros?.SedalenKathisma3?.Doxastichon != null)
                    {
                        stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(Orthros.SedalenKathisma3.Doxastichon) };
                    }
                    break;
                case PlaceYmnosSource.sedalen3_theotokion:
                    if (Orthros?.SedalenKathisma3?.Theotokion != null)
                    {
                        stichera = new YmnosStructure() { Theotokion = Orthros.SedalenKathisma3.Theotokion };
                    }
                    break;
            }

            return stichera;
        }
    }
}
