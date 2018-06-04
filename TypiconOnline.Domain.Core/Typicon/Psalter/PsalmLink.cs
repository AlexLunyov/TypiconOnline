using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Core.Typicon.Psalter
{
    public class PsalmLink : EntityBase<int>, IPsalterElement
    {
        public Psalm Psalm { get; set; }
        public int? StartStihos { get; set; }
        public int? EndStihos { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
