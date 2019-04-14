using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Days
{
    /// <summary>
    /// Абстрактный класс-родитель для описания текстов богослужения на дни Минеи и Триоди
    /// </summary>
    public abstract class Day : ValueObjectBase<ITypiconSerializer>, IHasId<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// Список последований служб
        /// </summary>
        public virtual List<DayWorship> DayWorships { get; set; }

        protected override void Validate(ITypiconSerializer serializer)
        {
            if (DayWorships != null)
            {
                foreach (DayWorship serv in DayWorships)
                {
                    if (!serv.IsValid(serializer))
                    {
                        AppendAllBrokenConstraints(serv, serializer);
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
