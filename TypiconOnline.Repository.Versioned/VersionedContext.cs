using Microsoft.EntityFrameworkCore;
using System;
using TypiconOnline.Domain.Versioned.Typicon;

namespace TypiconOnline.Repository.Versioned
{
    public class VersionedContext : DbContext
    {
        public VersionedContext(DbContextOptions<VersionedContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Typicon>();

            //TypiconVersion
            modelBuilder.Entity<TypiconVersion>()
                .HasOne(e => e.PreviousVersion)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.PreviousVersionId)
                .IsRequired(false); ;

            //Sign
            modelBuilder.Entity<Sign>()
                .HasMany(c => c.Versions)
                .WithOne(d => d.VersionOwner)
                .HasForeignKey(c => c.VersionOwnerId);

            modelBuilder.Entity<SignVersion>()
                .HasOne(e => e.PreviousVersion)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.PreviousVersionId)
                .IsRequired(false);

            modelBuilder.Entity<SignVersion>()
                .OwnsOne(c => c.Name, name =>
                {
                    name.OwnsMany(c => c.Items, items =>
                    {
                        items.Property<int>("NameId");
                        items.WithOwner().HasForeignKey("NameId");
                        items.Property<int>("Id");
                        items.HasKey("Id");
                        items.ToTable("SignNameItems");
                    });
                    //.ToTable("SignName");
                });
        }
    }
}
