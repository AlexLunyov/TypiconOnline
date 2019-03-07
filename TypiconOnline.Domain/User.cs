using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public class User : EntityBase<int>, IAggregateRoot
    {
        private User() { }

        public User(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }

        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

        /// <summary>
        /// Является ли Пользователь администратором системы
        /// </summary>
        public bool IsAdministrator { get; set; }
        /// <summary>
        /// Имеет ли Пользователь доступ к редактированию богослужебных текстов
        /// </summary>
        public bool IsTextEditor { get; set; }

        /// <summary>
        /// Уставы, созданные пользователем
        /// </summary>
        public virtual IEnumerable<Typicon.Typicon> OwnedTypicons { get; set; } = new List<Typicon.Typicon>();

        /// <summary>
        /// Список на промежуточную таблицу для редактируемых Уставов
        /// </summary>
        public virtual IEnumerable<UserTypicon> EditableUserTypicons { get; set; } = new List<UserTypicon>();

        /// <summary>
        /// Уставы, к которым имеется доступ в качестве Редактора
        /// </summary>
        public IEnumerable<Typicon.Typicon> EditableTypicons
        {
            get
            {
                return (from eut in EditableUserTypicons select eut.Typicon).ToList();
            }
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
