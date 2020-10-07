using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Context
{
    /// <summary>
    /// Контекст для исполнения веб-азпросов
    /// </summary>
    public class WebDbContext: TypiconDBContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<MenologyRuleGridModel> MenologyRuleModels { get; set; }
        public DbSet<TriodionRuleGridModel> TriodionRuleModels { get; set; }
        public DbSet<MenologyDayGridModel> MenologyDayModels { get; set; }
        public DbSet<TriodionDayGridModel> TriodionDayModels { get; set; }
        public DbSet<SignGridModel> Signs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
