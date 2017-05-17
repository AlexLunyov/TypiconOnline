using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Шаблонное правило. Используется для повторяющихся элементов.
    /// </summary>
    public class RuleTemplateEntity : RuleEntity
    {
        //public TypiconEntity TypiconEntity { get; set; }
        
            
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
