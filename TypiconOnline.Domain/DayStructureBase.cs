using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Базовый класс для определения богослужебных текстов
    /// </summary>
    public abstract class DayStructureBase : EntityBase<int>, IAggregateRoot
    {
        private DayContainer _dayContainer;
        private string _dayDefinition;

        /// <summary>
        /// Описание последовательности дня в xml-формате
        /// </summary>
        public virtual string DayDefinition
        {
            get
            {
                return _dayDefinition;
            }
            set
            {
                if (_dayDefinition != value)
                {
                    _dayDefinition = value;
                    _dayContainer = null;
                }
            }
        }

        /// <summary>
        /// Возвращает объектную модель определения богослужебного текста
        /// </summary>
        /// <returns></returns>
        public DayContainer GetDay()
        {
            ThrowExceptionIfInvalid();

            if (_dayContainer == null)
            {
                _dayContainer = new DayContainerFactory(_dayDefinition).Create();
            }

            return _dayContainer;
        }
    }
}
