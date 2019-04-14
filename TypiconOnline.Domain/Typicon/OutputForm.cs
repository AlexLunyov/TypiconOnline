using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Выходная форма для Последовательностей богослужений
    /// </summary>
    public class OutputForm : ValueObjectBase<ITypiconSerializer>, IHasId<int>
    {
        protected OutputForm() { }

        public OutputForm(int typiconId, DateTime date, string definition)
        {
            if (string.IsNullOrEmpty(definition))
            {
                throw new ArgumentException("message", nameof(definition));
            }

            TypiconId = typiconId;
            Date = date;
            Definition = definition;
        }

        public int Id { get; set; }

        public int TypiconId { get; set; }
        public virtual TypiconEntity Typicon { get; set; }

        public DateTime Date { get; set; }

        public virtual string Definition { get; set; }

        //Добавить ссылки на тексты служб
        public virtual List<OutputFormDayWorship> OutputFormDayWorships { get; set; }

        public IEnumerable<DayWorship> DayWorships
        {
            get
            {
                return (from drw in OutputFormDayWorships select drw.DayWorship).ToList();
            }
        }

        protected override void Validate(ITypiconSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
