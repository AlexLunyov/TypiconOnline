using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Infrastructure.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        //void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        //void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        //void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);

        IRepository<AggregateType> Repository<AggregateType>() where AggregateType : class, IAggregateRoot;

        void Commit();
    }
}
