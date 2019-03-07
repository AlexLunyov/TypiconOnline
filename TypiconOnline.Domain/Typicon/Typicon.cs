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

        public int OwnerId { get; set; }
        /// <summary>
        /// Владелец (создатель) Устава
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        /// Список на промежуточную таблицу для Редакторов Устава
        /// </summary>
        public virtual IEnumerable<UserTypicon> EditableUserTypicons { get; set; } = new List<UserTypicon>();
        
        /// <summary>
        /// Редакторы Устава
        /// </summary>
        public IEnumerable<User> Editors
        {
            get
            {
                return (from eut in EditableUserTypicons select eut.User).ToList();
            }
        }

        /// <summary>
        /// Версии Устава
        /// </summary>
        public virtual List<TypiconVersion> Versions { get; set; } = new List<TypiconVersion>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
