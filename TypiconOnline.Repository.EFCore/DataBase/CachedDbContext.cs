using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Repository.EFCore.Caching;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    /// <summary>
    /// Не работает
    /// </summary>
    public class CachedDbContext : TypiconDBContext
    {
        public CachedDbContext(DbContextOptions<TypiconDBContext> options) : base(options) { }

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
