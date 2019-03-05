using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Агрегат Устава, поддерживающий версионность
    /// </summary>
    public class Typicon : EntityBase<int>
    {
        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual Typicon Template { get; set; }
        /// <summary>
        /// Владелец (создатель) Устава
        /// </summary>
        public virtual User Owner { get; set; }
        /// <summary>
        /// Редакторы Устава
        /// </summary>
        public virtual IEnumerable<User> Editors { get; set; }

        /// <summary>
        /// Версии Устава
        /// </summary>
        public virtual IEnumerable<TypiconVersion> Versions { get; set; }// => _versions.ToList();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
