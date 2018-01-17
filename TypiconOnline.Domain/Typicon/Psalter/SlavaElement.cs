using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Psalter
{
    public class SlavaElement : EntityBase<int>, IPsalterElement
    {
        //нужен ли???
        public int Index { get; }
        public List<PsalmLink> PsalmLinks { get; set; } = new List<PsalmLink>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
