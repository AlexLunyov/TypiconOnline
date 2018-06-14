using System;
using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.Elements
{
    public class Makarios : DayElementBase
    {
        #region Properties

        public List<MakariosOdi> Odes { get; set; }

        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
