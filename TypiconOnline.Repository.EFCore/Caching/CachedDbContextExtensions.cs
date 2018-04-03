﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypiconOnline.Repository.EFCore.Caching
{
    /// <summary>
    /// Ref. https://github.com/VahidN/EFSecondLevelCache.Core EFChangeTrackerExtensions.cs
    /// </summary>
    public static class CachedDbContextExtensions
    {
        /// <summary>
        /// Using the ChangeTracker to find names of the changed entities.
        /// </summary>
        public static string[] GetChangedEntityNames(this DbContext dbContext)
        {
            var typesList = new List<Type>();
            foreach (var type in dbContext.GetChangedEntityTypes())
            {
                typesList.Add(type);
                //typesList.AddRange(type.GetBaseTypes().Where(t => t != typeof(object)).ToList());
            }

            var changedEntityNames = typesList
                .Select(type => type.FullName)
                .Distinct()
                .ToArray();

            return changedEntityNames;
        }

        public static string[] GetChangedEntityNames<T>(this DbContext dbContext)
        {
            var typesList = new List<Type>();
            foreach (var type in dbContext.GetChangedEntityTypes<T>())
            {
                typesList.Add(type);
            }

            var changedEntityNames = typesList
                .Select(type => type.FullName)
                .Distinct()
                .ToArray();

            return changedEntityNames;
        }

        public static IEnumerable<Type> GetChangedEntityTypes<T>(this DbContext dbContext)
        {
            return dbContext.ChangeTracker.Entries().Where(
                            dbEntityEntry => dbEntityEntry.State == EntityState.Added ||
                            dbEntityEntry.State == EntityState.Modified ||
                            dbEntityEntry.State == EntityState.Deleted)
                .Select(dbEntityEntry => dbEntityEntry.Entity.GetType())
                .Where(t => t.GetBaseTypes().Any(c => c.Equals(typeof(T))));
        }

        /// <summary>
        /// Using the ChangeTracker to find types of the changed entities.
        /// </summary>
        public static IEnumerable<Type> GetChangedEntityTypes(this DbContext dbContext)
        {
            return dbContext.ChangeTracker.Entries().Where(
                            dbEntityEntry => dbEntityEntry.State == EntityState.Added ||
                            dbEntityEntry.State == EntityState.Modified ||
                            dbEntityEntry.State == EntityState.Deleted)
                .Select(dbEntityEntry => dbEntityEntry.Entity.GetType());
        }


        /// <summary>
        /// Find the base types of the given type, recursively.
        /// </summary>
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            if (type.GetTypeInfo().BaseType == null)
            {
                return type.GetInterfaces();
            }

            return Enumerable.Repeat(type.GetTypeInfo().BaseType, 1)
                             .Concat(type.GetInterfaces())
                             .Concat(type.GetInterfaces().SelectMany(GetBaseTypes))
                             .Concat(type.GetTypeInfo().BaseType.GetBaseTypes());
        }
    }
}
