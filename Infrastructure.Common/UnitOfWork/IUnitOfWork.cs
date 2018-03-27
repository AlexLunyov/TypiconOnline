using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Infrastructure.Common.UnitOfWork
{
    public interface IUnitOfWork //: IDisposable
    {
        IRepository<AggregateType> Repository<AggregateType>() where AggregateType : class, IAggregateRoot;

        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
