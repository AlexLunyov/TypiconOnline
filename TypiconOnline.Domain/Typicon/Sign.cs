using System;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Typicon
{
    public class Sign : TypiconRule/*<ExecContainer>*/
    {
        public int Number { get; set; }
        public int Priority { get; set; }

        public bool IsTemplate { get; set; }

        public virtual TypiconEntity TypiconEntity { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

    }
}

