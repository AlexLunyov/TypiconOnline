using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Typicon
{
    public class Sign : TypiconRule
    {
        public int Number { get; set; }
        public int Priority { get; set; }

        public bool IsTemplate { get; set; }

        /// <summary>
        /// Наимнование знака службы на нескольких языках
        /// </summary>
        public ItemTextStyled SignName { get; set; }

        public override string Name
        {
            get
            {
                return SignName[(Owner != null) ? Owner.Settings.DefaultLanguage : ""];
            }
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

    }
}

