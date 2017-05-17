using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    /// <summary>
    /// Хранилище объектов ModifiedYear, объединенных по году
    /// </summary>
    public class ModifiedYear : EntityBase<int>, IAggregateRoot
    {
        public ModifiedYear()
        {
            ModifiedRules = new List<ModifiedRule>();
        }

        public virtual TypiconEntity TypiconEntity { get; set; }

        public int Year { get; set; }

        public virtual List<ModifiedRule> ModifiedRules { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
