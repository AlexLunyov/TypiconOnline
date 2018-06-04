using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class DayContainerExtensions
    {
        /// <summary>
        /// Возвращает тексты из определенного места в службе
        /// </summary>
        /// <param name="place">Место</param>
        /// <param name="count">Количество</param>
        /// <param name="startFrom">С какого по номеру песнопения начинать выборку</param>
        /// <returns></returns>
        public static YmnosGroupCollection GetYmnosGroups(this DayContainer container, PlaceYmnosSource place, int count, int startFrom)
        {
            container.ThrowExceptionIfInvalid();

            YmnosGroupCollection result = default(YmnosGroupCollection);

            switch (place)
            {
                //kekragaria
                case PlaceYmnosSource.kekragaria:
                    result = container.Esperinos?.Kekragaria?.Groups.GetYmnis(count, startFrom);
                    break;
                case PlaceYmnosSource.kekragaria_doxastichon:
                    {
                        result = GetDoxastichon(container.Esperinos?.Kekragaria);
                    }
                    break;
                case PlaceYmnosSource.kekragaria_theotokion:
                    {
                        result = GetTheotokion(container.Esperinos?.Kekragaria);
                    }
                    break;
                case PlaceYmnosSource.kekragaria_stavrostheotokion:
                    {
                        result = GetTheotokion(container.Esperinos?.Kekragaria, YmnosGroupKind.Stavros);
                    }
                    break;
                //liti
                case PlaceYmnosSource.liti:
                    result = container.Esperinos?.Liti?.Groups.GetYmnis(count, startFrom);
                    break;
                case PlaceYmnosSource.liti_doxastichon:
                    {
                        result = GetDoxastichon(container.Esperinos?.Liti);
                    }
                    break;
                case PlaceYmnosSource.liti_theotokion:
                    {
                        result = GetTheotokion(container.Esperinos?.Liti);
                    }
                    break;
                //aposticha_esperinos
                case PlaceYmnosSource.aposticha_esperinos:
                    result = container.Esperinos?.Aposticha?.Groups.GetYmnis(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_esperinos_doxastichon:
                    {
                        result = GetDoxastichon(container.Esperinos?.Aposticha);
                    }
                    break;
                case PlaceYmnosSource.aposticha_esperinos_theotokion:
                    {
                        result = GetTheotokion(container.Esperinos?.Aposticha);
                    }
                    break;
                //ainoi
                case PlaceYmnosSource.ainoi:
                    result = container.Orthros?.Ainoi?.Groups.GetYmnis(count, startFrom);
                    break;
                case PlaceYmnosSource.ainoi_doxastichon:
                    {
                        result = GetDoxastichon(container.Orthros?.Ainoi);
                    }
                    break;
                case PlaceYmnosSource.ainoi_theotokion:
                    {
                        result = GetTheotokion(container.Orthros?.Ainoi);
                    }
                    break;
                //aposticha_orthros
                case PlaceYmnosSource.aposticha_orthros:
                    result = container.Orthros?.Aposticha?.Groups.GetYmnis(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_orthros_doxastichon:
                    {
                        result = GetDoxastichon(container.Orthros?.Aposticha);
                    }
                    break;
                case PlaceYmnosSource.aposticha_orthros_theotokion:
                    {
                        result = GetTheotokion(container.Orthros?.Aposticha);
                    }
                    break;
                //troparion
                case PlaceYmnosSource.troparion:
                    {
                        //Выбираем либо из Малой вечерни, либо с Вечерни тропарь
                        YmnosStructure y = container.MikrosEsperinos?.Troparion ?? container.Esperinos.Troparion ?? null;

                        result = y?.Groups.GetYmnis(count, startFrom);
                    }
                    break;
                case PlaceYmnosSource.troparion_doxastichon:
                    {
                        result = GetDoxastichon(container.MikrosEsperinos?.Troparion ?? container.Esperinos?.Troparion);
                    }
                    break;
                case PlaceYmnosSource.troparion_theotokion:
                    {
                        result = GetTheotokion(container.MikrosEsperinos?.Troparion ?? container.Esperinos?.Troparion);
                    }
                    break;

                //sedalen1
                case PlaceYmnosSource.sedalen1:
                    {
                        result = container.Orthros?.SedalenKathisma1?.Groups.GetYmnis(count, startFrom);
                    }
                    break;
                case PlaceYmnosSource.sedalen1_doxastichon:
                    {
                        result = GetDoxastichon(container.Orthros?.SedalenKathisma1);
                    }
                    break;
                case PlaceYmnosSource.sedalen1_theotokion:
                    {
                        result = GetTheotokion(container.Orthros?.SedalenKathisma1);
                    }
                    break;

                //sedalen2
                case PlaceYmnosSource.sedalen2:
                    {
                        result = container.Orthros?.SedalenKathisma2?.Groups.GetYmnis(count, startFrom);
                    }
                    break;
                case PlaceYmnosSource.sedalen2_doxastichon:
                    {
                        result = GetDoxastichon(container.Orthros?.SedalenKathisma2);
                    }
                    break;
                case PlaceYmnosSource.sedalen2_theotokion:
                    {
                        result = GetTheotokion(container.Orthros?.SedalenKathisma2);
                    }
                    break;

                //sedalen3
                case PlaceYmnosSource.sedalen3:
                    {
                        result = container.Orthros?.SedalenKathisma3?.Groups.GetYmnis(count, startFrom);
                    }
                    break;
                case PlaceYmnosSource.sedalen3_doxastichon:
                    {
                        result = GetDoxastichon(container.Orthros?.SedalenKathisma3);
                    }
                    break;
                case PlaceYmnosSource.sedalen3_theotokion:
                    {
                        result = GetTheotokion(container.Orthros?.SedalenKathisma3);
                    }
                    break;

                //sedalen_kanonas
                case PlaceYmnosSource.sedalen_kanonas:
                    {
                        result = container.Orthros?.SedalenKanonas?.Groups.GetYmnis(count, startFrom);
                    }
                    break;
                case PlaceYmnosSource.sedalen_kanonas_theotokion:
                    {
                        result = GetTheotokion(container.Orthros?.SedalenKanonas);
                    }
                    break;
                case PlaceYmnosSource.sedalen_kanonas_stavrostheotokion:
                    {
                        result = GetTheotokion(container.Orthros?.SedalenKanonas, YmnosGroupKind.Stavros);
                    }
                    break;
            }

            return result;

            YmnosGroupCollection GetDoxastichon(YmnosStructure structure)
            {
                var found = structure?.Doxastichon?.GetGroupWithSingleYmnos(0);

                return (structure?.Doxastichon is YmnosGroup d) ? new YmnosGroupCollection() { new YmnosGroup(d) } : default(YmnosGroupCollection);
            }

            YmnosGroupCollection GetTheotokion(YmnosStructure structure, YmnosGroupKind kind = YmnosGroupKind.Undefined)
            {
                var found = structure?.Theotokion?.FirstOrDefault(c => c.Kind == kind)?.GetGroupWithSingleYmnos(0);

                return (found != null) ? new YmnosGroupCollection() { found } : default(YmnosGroupCollection);
            }

        }
    }
}
