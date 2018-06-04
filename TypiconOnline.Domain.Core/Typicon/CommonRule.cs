using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Core.Typicon
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
    }
}

