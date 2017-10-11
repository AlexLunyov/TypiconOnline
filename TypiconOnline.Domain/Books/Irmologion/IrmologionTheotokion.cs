using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Irmologion
{
    /// <summary>
    /// Элемент из Богородичных приложений Ирмология
    /// </summary>
    public class IrmologionTheotokion : EntityBase<int>, IAggregateRoot, IBookElement<Ymnos> 
    {
        public virtual PlaceYmnosSource Place { get; set; }
        public virtual int Ihos { get; set; }
        public virtual DayOfWeek DayOfWeek { get; set; }
        public string StringDefinition { get; set; }

        protected override void Validate()
        {
            if (Ihos < 1 || Ihos > 8)
            {
                AddBrokenConstraint(IrmologionTheotokionBusinessConstraint.InvalidIhos);
            }

            if (string.IsNullOrEmpty(StringDefinition))
            {
                AddBrokenConstraint(IrmologionTheotokionBusinessConstraint.EmptyStringDefinition);
            }
        }

        public Ymnos GetElement()
        {
            ThrowExceptionIfInvalid();

            return new TypiconSerializer().Deserialize<Ymnos>(StringDefinition);
        }
    }
}
