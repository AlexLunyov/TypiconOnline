using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.AppServices.Implementations
{
    public class DayContainerService : IDayContainerService
    {
        private DayContainer _dayContainer;


        public DayContainerService(DayContainer dayContainer)
        {
            if (dayContainer == null) throw new ArgumentNullException("DayContainer");

            _dayContainer = dayContainer;
        }

        public DayContainer DayContainer
        {
            get
            {
                return _dayContainer;
            }
            set
            {
                if (value != null)
                {
                    _dayContainer = value;
                }
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
            YmnosStructure stichera = null;

            switch (place)
            {
                //kekragaria
                case PlaceYmnosSource.kekragaria:
                    stichera = _dayContainer.Esperinos.Kekragaria.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.kekragaria_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(_dayContainer.Esperinos.Kekragaria.Doxastichon) };
                    break;
                case PlaceYmnosSource.kekragaria_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = _dayContainer.Esperinos.Kekragaria.Theotokion };
                    break;
                //liti
                case PlaceYmnosSource.liti:
                    stichera = _dayContainer.Esperinos.Liti.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.liti_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(_dayContainer.Esperinos.Liti.Doxastichon) };
                    break;
                case PlaceYmnosSource.liti_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = _dayContainer.Esperinos.Liti.Theotokion };
                    break;
                //aposticha_esperinos
                case PlaceYmnosSource.aposticha_esperinos:
                    stichera = _dayContainer.Esperinos.Aposticha.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_esperinos_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(_dayContainer.Esperinos.Aposticha.Doxastichon) };
                    break;
                case PlaceYmnosSource.aposticha_esperinos_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = _dayContainer.Esperinos.Aposticha.Theotokion };
                    break;
                //ainoi
                case PlaceYmnosSource.ainoi:
                    stichera = _dayContainer.Orthros.Ainoi.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.ainoi_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(_dayContainer.Orthros.Ainoi.Doxastichon) };
                    break;
                case PlaceYmnosSource.ainoi_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = _dayContainer.Orthros.Ainoi.Theotokion };
                    break;
                //aposticha_orthros
                case PlaceYmnosSource.aposticha_orthros:
                    stichera = _dayContainer.Orthros.Aposticha.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_orthros_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup(_dayContainer.Orthros.Aposticha.Doxastichon) };
                    break;
                case PlaceYmnosSource.aposticha_orthros_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = _dayContainer.Orthros.Aposticha.Theotokion };
                    break;
            }

            return stichera;
        }
    }
}
