using System;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconSettings : EntityBase<int>
    {
        public virtual int TypiconEntityId { get; set; }

        public virtual TypiconEntity TypiconEntity { get; set; }

        public virtual string DefaultLanguage { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}