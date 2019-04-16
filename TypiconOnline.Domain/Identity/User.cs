using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Identity
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// Полное "человеческое" имя для отображения
        /// </summary>
        public string FullName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Уставы, созданные пользователем
        /// </summary>
        public virtual IEnumerable<TypiconEntity> OwnedTypicons { get; set; } = new List<TypiconEntity>();

        /// <summary>
        /// Список на промежуточную таблицу для редактируемых Уставов
        /// </summary>
        public virtual IEnumerable<UserTypicon> EditableUserTypicons { get; set; } = new List<UserTypicon>();

        /// <summary>
        /// Уставы, к которым имеется доступ в качестве Редактора
        /// </summary>
        public IEnumerable<TypiconEntity> EditableTypicons
        {
            get
            {
                return (from eut in EditableUserTypicons select eut.Typicon).ToList();
            }
        }
    }
}
