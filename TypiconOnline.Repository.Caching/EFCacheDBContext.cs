using EFSecondLevelCache.Core;
using EFSecondLevelCache.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Repository.Caching
{
    public class EFCacheDBContext : TypiconDBContext
    {
        readonly IEFCacheServiceProvider serviceProvider;

        public EFCacheDBContext(DbContextOptions<TypiconDBContext> options, IEFCacheServiceProvider serviceProvider) : base(options)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException("serviceProvider in EFCacheDBContext");
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var changedEntityNames = this.GetChangedEntityNames();

            this.ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            this.ChangeTracker.AutoDetectChangesEnabled = true;

            serviceProvider.InvalidateCacheDependencies(changedEntityNames);

            return result;
        }

    }
}
