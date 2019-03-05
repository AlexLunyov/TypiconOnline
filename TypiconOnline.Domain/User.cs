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
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

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
        public IEnumerable<Typicon.Typicon> OwnedTypicons { get; set; }

        /// <summary>
        /// Уставы, к которым имеется доступ в качестве Редактора
        /// </summary>
        public IEnumerable<Typicon.Typicon> EditableTypicons { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
