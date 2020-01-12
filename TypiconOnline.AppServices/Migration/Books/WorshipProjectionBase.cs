using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Migration.Books
{
    public class WorshipProjectionBase
    {
        public int Id { get; set; }

        public ItemTextStyled WorshipName { get; set; }

        public ItemText WorshipShortName { get; set; }

        public virtual bool UseFullName { get; set; }

        public virtual bool IsCelebrating { get; set; }
    }
}
