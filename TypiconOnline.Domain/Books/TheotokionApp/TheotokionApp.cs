using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.TheotokionApp
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
