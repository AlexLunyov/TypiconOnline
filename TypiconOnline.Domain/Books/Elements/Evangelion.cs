using System;
using System.Collections.Generic;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Евангельское чтение
    /// </summary>
    public class Evangelion : DayElementBase
    {
        public Evangelion() { }

        #region Properties

        public List<EvangelionPart> Parts { get; set; }

        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
