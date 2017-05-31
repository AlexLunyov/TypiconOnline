using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Folders
{
    public abstract class BaseFolder<T>: EntityBase<int>, IAggregateRoot where T : RuleEntity
    {
        public BaseFolder()
        {
            ChildElements = new List<T>();
        }
        public virtual string Name { get; set; }
        public List<T> ChildElements { get; set; }
    }
}
