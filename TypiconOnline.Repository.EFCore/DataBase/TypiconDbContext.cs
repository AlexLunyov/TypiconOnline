using EFSecondLevelCache.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class CachedDbContext : DBContextBase
    {
        public CachedDbContext(DbContextOptions<DBContextBase> options) : base(options) { }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var changedEntityNames = this.GetChangedEntityNames();

            this.ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            this.ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }
    }
}
