using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Core.Days
{
    /// <summary>
    /// Абстрактный класс-родитель для описания текстов богослужения на дни Минеи и Триоди
    /// </summary>
    public abstract class Day : EntityBase<int>, IAggregateRoot
    {
        /// <summary>
        /// Список последований служб
        /// </summary>
        public virtual List<DayWorship> DayWorships { get; set; }

        protected override void Validate()
        {
            if (DayWorships != null)
            {
                foreach (DayWorship serv in DayWorships)
                {
                    if (!serv.IsValid)
                    {
                        AppendAllBrokenConstraints(serv);
                    }
                }
            }
        }

        public void AppendDayService(DayWorship serv)
        {
            if (DayWorships == null)
            {
                DayWorships = new List<DayWorship>();
            }

            serv.Parent = this;

            DayWorships.Add(serv);
        }
    }
}
