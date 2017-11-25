using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Правило для описания повторяющихся элементов, которые можно в дальнейшем неоднократно использовать.
    /// Пример: ектении
    /// </summary>
    public class CommonRule : RuleEntity
    {
        public int OwnerId { get; set; }

        public virtual TypiconEntity Owner { get; set; }
        
        
    }
}

