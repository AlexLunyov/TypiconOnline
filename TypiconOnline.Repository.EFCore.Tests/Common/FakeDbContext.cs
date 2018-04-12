using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Repository.EFCore.Caching;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.EFCore.Tests.Common
{
    public class FakeDbContext : TypiconDBContext
    {
        public FakeDbContext(DbContextOptions<TypiconDBContext> options) : base(options) { }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var changedEntityNames = this.GetChangedEntityNames<IAggregateRoot>();

            var ch = ChangeTracker.Entries().ToList();

            this.ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            //do nothing
            //var result = base.SaveChanges();
            this.ChangeTracker.AutoDetectChangesEnabled = true;

            return 0;
        }
    }
}
