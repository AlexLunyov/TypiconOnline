using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    public class Typicon : IHasId<int>
    {
        public int Id { get; set; }

        public virtual List<TypiconVersion> Versions { get; set; } = new List<TypiconVersion>();

        public virtual List<Sign> Signs { get; set; } = new List<Sign>();
    }
}
