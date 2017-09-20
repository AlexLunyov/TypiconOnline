
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Days
{
    /// <summary>
    /// Описание текста службы празднику
    /// </summary>
    public class DayService : EntityBase<int>, IAggregateRoot
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
        /// <summary>
        /// Описание последовательности дня в xml-формате
        /// </summary>
        public virtual string DayDefinition { get; set; }

        protected override void Validate()
        {
            if (!ServiceName.IsValid)//(ServiceName?.IsValid == false)
            {
                AppendAllBrokenConstraints(ServiceName);
            }
        }

        /*
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
                    stichera = (Rule as DayContainer).Esperinos.Kekragaria.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.kekragaria_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup((Rule as DayContainer).Esperinos.Kekragaria.Doxastichon) };
                    break;
                case PlaceYmnosSource.kekragaria_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = (Rule as DayContainer).Esperinos.Kekragaria.Theotokion }; 
                    break;
                //liti
                case PlaceYmnosSource.liti:
                    stichera = (Rule as DayContainer).Esperinos.Liti.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.liti_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup((Rule as DayContainer).Esperinos.Liti.Doxastichon) }; 
                    break;
                case PlaceYmnosSource.liti_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = (Rule as DayContainer).Esperinos.Liti.Theotokion };
                    break;
                //aposticha_esperinos
                case PlaceYmnosSource.aposticha_esperinos:
                    stichera = (Rule as DayContainer).Esperinos.Aposticha.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_esperinos_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup((Rule as DayContainer).Esperinos.Aposticha.Doxastichon) };
                    break;
                case PlaceYmnosSource.aposticha_esperinos_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = (Rule as DayContainer).Esperinos.Aposticha.Theotokion };
                    break;
                //ainoi
                case PlaceYmnosSource.ainoi:
                    stichera = (Rule as DayContainer).Orthros.Ainoi.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.ainoi_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup((Rule as DayContainer).Orthros.Ainoi.Doxastichon) };
                    break;
                case PlaceYmnosSource.ainoi_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = (Rule as DayContainer).Orthros.Ainoi.Theotokion };
                    break;
                //aposticha_orthros
                case PlaceYmnosSource.aposticha_orthros:
                    stichera = (Rule as DayContainer).Orthros.Aposticha.GetYmnosStructure(count, startFrom);
                    break;
                case PlaceYmnosSource.aposticha_orthros_doxastichon:
                    stichera = new YmnosStructure() { Doxastichon = new YmnosGroup((Rule as DayContainer).Orthros.Aposticha.Doxastichon) };
                    break;
                case PlaceYmnosSource.aposticha_orthros_theotokion:
                    //TODO: прамая ссылка без копирования и клонирования
                    stichera = new YmnosStructure() { Theotokion = (Rule as DayContainer).Orthros.Aposticha.Theotokion };
                    break;
            }

            return stichera;
        }*/

    }
}

