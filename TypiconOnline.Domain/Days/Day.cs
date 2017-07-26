using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Days
{
    /// <summary>
    /// Абстрактный класс-родитель для описания текстов богослужения на дни Минеи и Триоди
    /// </summary>
    public abstract class Day : EntityBase<int>, IAggregateRoot
    {
        /// <summary>
        /// Список последований служб
        /// </summary>
        public virtual List<DayService> DayServices { get; set; }

        protected override void Validate()
        {
            if (DayServices != null)
            {
                foreach (DayService serv in DayServices)
                {
                    if (!serv.IsValid)
                    {
                        AppendAllBrokenConstraints(serv);
                    }
                }
            }
        }

        public void AppendDayService(DayService serv)
        {
            if (DayServices == null)
            {
                DayServices = new List<DayService>();
            }

            serv.Parent = this;

            DayServices.Add(serv);
        }
    }
}
