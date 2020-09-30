using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;

namespace TypiconOnline.Domain.WebQuery.Context
{
    /// <summary>
    /// Контекст для исполнения веб-азпросов
    /// </summary>
    public class WebDbContext: DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<MenologyRuleGridModel> MenologyRules { get; set; }
        public DbSet<TriodionRuleGridModel> TriodionRules { get; set; }
        public DbSet<MenologyDayGridModel> MenologyDays { get; set; }
        public DbSet<TriodionDayGridModel> TriodionDays { get; set; }
        public DbSet<SignGridModel> Signs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenologyRuleGridModel>((pc =>
            {
                pc.HasNoKey();
                pc.ToView("View_MenologyRuleGrid");
            }));

            modelBuilder.Entity<TriodionRuleGridModel>((pc =>
            {
                pc.HasNoKey();
                pc.ToView("View_TriodionRuleGrid");
            }));

            modelBuilder.Entity<MenologyDayGridModel>((pc =>
            {
                pc.HasNoKey();
                pc.ToView("View_MenologyDayGrid");
            }));

            modelBuilder.Entity<TriodionDayGridModel>((pc =>
            {
                pc.HasNoKey();
                pc.ToView("View_TriodionDayGrid");
            }));

            modelBuilder.Entity<SignGridModel>((pc =>
            {
                pc.HasNoKey();
                pc.ToView("View_SignGrid");
            }));
        }
    }
}
