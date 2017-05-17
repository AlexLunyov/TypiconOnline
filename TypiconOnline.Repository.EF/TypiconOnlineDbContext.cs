using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.Entity;
using System.Data.Common;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Repository.EF
{
    class TypiconOnlineDbContext : DbContext//, IUnitOfWork
    {


        public TypiconOnlineDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configure();
        }

        public TypiconOnlineDbContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
            var initializer = new TypiconOnlineDbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }

        public void RegisterUpdate(IAggregateRoot aggregateRoot)
        {
            //Database.
        }

        public void RegisterInsertion(IAggregateRoot aggregateRoot)
        {
            //if (!_insertedAggregates.ContainsKey(aggregateRoot))
            //{
            //    _insertedAggregates.Add(aggregateRoot, repository);
            //}
        }

        public void RegisterDeletion(IAggregateRoot aggregateRoot)
        {
            //if (!_deletedAggregates.ContainsKey(aggregateRoot))
            //{
            //    _deletedAggregates.Add(aggregateRoot, repository);
            //}
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
