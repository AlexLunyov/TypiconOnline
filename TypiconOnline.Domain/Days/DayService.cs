﻿
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;

namespace TypiconOnline.Domain.Days
{
    /// <summary>
    /// Описание текста службы празднику
    /// </summary>
    public class DayService : RuleEntity
    {
        public DayService()
        {
            ServiceName = new ItemText();
        }

        /// <summary>
        /// Наименование праздника
        /// </summary>
        public virtual ItemText ServiceName { get; set; }
        /// <summary>
        /// Признак Господского или Богородичного праздника, его предпразднества или попразднества
        /// </summary>
        public virtual bool IsCelebrating { get; set; }
        /// <summary>
        /// День Минеи или Триоди, которому принадлежит данное описание текста службы
        /// </summary>
        public virtual Day Parent { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (!ServiceName.IsValid)//(ServiceName?.IsValid == false)
            {
                AppendAllBrokenConstraints(ServiceName);
            }
        }

        /// <summary>
        /// Возвращает тексты из определенного места в службе
        /// </summary>
        /// <param name="place">Место</param>
        /// <param name="count">Количество</param>
        /// <param name="startFrom">С какого по номеру песнопения начинать выборку</param>
        /// <returns></returns>
        public Stichera GetStichera(PlaceYmnosSource place, int count, int startFrom)
        {
            ThrowExceptionIfInvalid();

            Stichera stichera = null;

            switch (place)
            {
                //kekragaria
                case PlaceYmnosSource.kekragaria:
                    stichera = (Rule as DayElement).Esperinos.Kekragaria.GetStichera(count, startFrom);
                    break;
                case PlaceYmnosSource.kekragaria_doxastichon:
                    stichera = new Stichera((Rule as DayElement).Esperinos.Kekragaria.Doxastichon);
                    break;
                case PlaceYmnosSource.kekragaria_theotokion:
                    stichera = new Stichera((Rule as DayElement).Esperinos.Kekragaria.Theotokion);
                    break;
                //liti
                case PlaceYmnosSource.liti:
                    stichera = (Rule as DayElement).Esperinos.Liti.GetStichera(count, startFrom);
                    break;
                case PlaceYmnosSource.liti_doxastichon:
                    stichera = new Stichera((Rule as DayElement).Esperinos.Liti.Doxastichon);
                    break;
                case PlaceYmnosSource.liti_theotokion:
                    stichera = new Stichera((Rule as DayElement).Esperinos.Liti.Theotokion);
                    break;
                //aposticha_esperinos
                case PlaceYmnosSource.aposticha_esperinos:
                    stichera = (Rule as DayElement).Esperinos.Aposticha.GetStichera(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_esperinos_doxastichon:
                    stichera = new Stichera((Rule as DayElement).Esperinos.Aposticha.Doxastichon);
                    break;
                case PlaceYmnosSource.aposticha_esperinos_theotokion:
                    stichera = new Stichera((Rule as DayElement).Esperinos.Aposticha.Theotokion);
                    break;
                //ainoi
                case PlaceYmnosSource.ainoi:
                    stichera = (Rule as DayElement).Orthros.Ainoi.GetStichera(count, startFrom);
                    break;
                case PlaceYmnosSource.ainoi_doxastichon:
                    stichera = new Stichera((Rule as DayElement).Orthros.Ainoi.Doxastichon);
                    break;
                case PlaceYmnosSource.ainoi_theotokion:
                    stichera = new Stichera((Rule as DayElement).Orthros.Ainoi.Theotokion);
                    break;
                //aposticha_orthros
                case PlaceYmnosSource.aposticha_orthros:
                    stichera = (Rule as DayElement).Orthros.Aposticha.GetStichera(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_orthros_doxastichon:
                    stichera = new Stichera((Rule as DayElement).Orthros.Aposticha.Doxastichon);
                    break;
                case PlaceYmnosSource.aposticha_orthros_theotokion:
                    stichera = new Stichera((Rule as DayElement).Orthros.Aposticha.Theotokion);
                    break;
            }

            return stichera;
        }

    }
}

