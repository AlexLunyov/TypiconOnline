using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Repository.Versioned
{
    public class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
            //Database.EnsureCreated();
            
            //Database.ExecuteSqlRaw(
            //@"CREATE VIEW View_SignDraftVersions AS 
            //    SELECT s.Id, s.TypiconId, v.RuleDefinition, v.ModRuleDefinition, v.TemplateId, v.Priority, v.IsAddition
            //    FROM Sign s
            //    INNER JOIN SignVersion v ON s.Id = v.VersionOwnerId
            //    WHERE v.PublishDate IS NULL and v.ArchiveDate IS NULL");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //привязка к view с опубликованными версиями Знаков службы
            modelBuilder.Entity<Domain.Typicon.Sign>((c =>
            {
                c.HasNoKey();
                c.Ignore(s => s.TypiconVersion);
                c.Ignore(s => s.PrintTemplate);
                c.Ignore(s => s.PrintTemplateLinks);
                c.Ignore(s => s.VariableLinks);
                c.Ignore(s => s.Template);

                //c.HasOne(e => e.Template).
                //   WithMany()
                //   .OnDelete(DeleteBehavior.Cascade)
                //   .HasForeignKey(c => c.TemplateId)
                //   .IsRequired(false);

                //c.OwnsOne(s => s.SignName, name =>
                // {
                //     name.OwnsMany(s => s.Items, items =>
                //     {
                //         items.Property<int>("NameId");
                //         items.WithOwner().HasForeignKey("NameId");
                //         items.Property<int>("Id");
                //         items.HasKey("Id");
                //         items.ToTable("SignNameItems");
                //     });
                //     //.ToTable("SignName");
                // });

                c.ToView("View_SignDraftVersions");
            }));
        }
    }
}
