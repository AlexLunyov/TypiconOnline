using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Versioned.Typicon
{
    /// <summary>
    /// Базовый класс для вложенных элементов Устава
    /// </summary>
    public abstract class RuleBase<T> : EntityBase, IHasId<int> where T: VersionBase, new()
    {
        public int Id { get; set; }

        public int TypiconId { get; set; }

        public virtual Typicon Typicon { get; set; }

        public virtual List<T> Versions { get; set; } = new List<T>();
    }
}
