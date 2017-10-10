using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Irmologion
{
    public class IrmologionTheotokion : EntityBase<int>, IAggregateRoot
    {
        public virtual PlaceYmnosSource Place { get; set; }
        public virtual int Ihos { get; set; }
        public virtual DayOfWeek DayOfWeek { get; set; }
        public string StringDefinition { get; set; }

        protected override void Validate()
        {
            if (Ihos < 1 || Ihos > 8)
            {

            }
        }
    }
}
