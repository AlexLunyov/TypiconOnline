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
    public class User : IHasId<int>, IAggregateRoot
    {
        public User() { }

        public User(string userName, string login, string passwordHash)
        {
            UserName = userName;
            Login = login;
            PasswordHash = passwordHash;
        }

        public int Id { get; set; }

        public string UserName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Является ли Пользователь администратором системы
        /// </summary>
        public bool IsAdministrator { get; set; }
        /// <summary>
        /// Имеет ли Пользователь доступ к редактированию богослужебных текстов
        /// </summary>
        public bool IsTextEditor { get; set; }

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
