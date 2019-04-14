using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Правило для описания повторяющихся элементов, которые можно в дальнейшем неоднократно использовать.
    /// Пример: ектении
    /// </summary>
    public class CommonRule : RuleEntity
    {
        /// <summary>
        /// Наименование Общего правила.
        /// Должно быть уникальным в рамках Устава
        /// </summary>
        public virtual string Name { get; set; }

        protected override void Validate(IRuleSerializerRoot serializerRoot)
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(new BusinessConstraint("Общее Правило: Наименование обязательно для заполнения."));
            }
        }
    }
}

