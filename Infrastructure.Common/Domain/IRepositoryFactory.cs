using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public interface IRepositoryFactory
    {
        IRepository<AggregateType> Create<AggregateType>() where AggregateType : class, IAggregateRoot;
    }
}
