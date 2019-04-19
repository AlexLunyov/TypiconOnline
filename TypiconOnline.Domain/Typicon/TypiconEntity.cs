using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Агрегат Устава, поддерживающий версионность
    /// </summary>
    public class TypiconEntity : ValueObjectBase, IHasId<int>
    {
        public TypiconEntity()
        {
            Status = TypiconStatus.WaitingApprovement;
        }

        public int Id { get; set; }

        public virtual ItemText Name { get; set; }
        /// <summary>
        /// Язык по умолчанию
        /// </summary>
        public virtual string DefaultLanguage { get; set; }
        public int? TemplateId { get; set; }
        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual TypiconEntity Template { get; set; }

        public int OwnerId { get; set; }
        /// <summary>
        /// Владелец (создатель) Устава
        /// </summary>
        public virtual User Owner { get; set; }

        public TypiconStatus Status { get; set; }

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
            if (Name == null || Name.IsEmpty)
            {
                AddBrokenConstraint(new BusinessConstraint("Имя Устава должно быть определено"));
            }
            else if (!Name.IsValid)
            {
                AppendAllBrokenConstraints(Name);
            }

            if (string.IsNullOrEmpty(DefaultLanguage)
                || !ItemText.IsLanguageValid(DefaultLanguage))
            {
                AddBrokenConstraint(new BusinessConstraint("Значения языка по умолчанию указано неверно"));
            }
        }
    }

    public enum TypiconStatus
    {
        WaitingApprovement = 0,
        Approving = 1,
        Draft = 2,
        Validating = 3,
        Publishing = 4,
        Published = 5
    }
}
