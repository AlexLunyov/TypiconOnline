using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Infrastructure.Common.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        void PersistInsertion(IAggregateRoot aggregateRoot);
        void PersistUpdate(IAggregateRoot aggregateRoot);
        void PersistDeletion(IAggregateRoot aggregateRoot);
    }
}