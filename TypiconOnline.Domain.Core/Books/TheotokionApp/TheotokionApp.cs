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

namespace TypiconOnline.Domain.Core.Books.TheotokionApp
{
    /// <summary>
    /// Элемент из Богородичных приложений Ирмология
    /// </summary>
    public class TheotokionApp : BookElementBase<Ymnos>, IAggregateRoot
    {
        public virtual TheotokionAppPlace Place { get; set; }
        public virtual int Ihos { get; set; }
        public virtual DayOfWeek DayOfWeek { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (Ihos < 1 || Ihos > 8)
            {
                AddBrokenConstraint(TheotokionAppBusinessConstraint.InvalidIhos);
            }
        }
    }
}
