using Microsoft.EntityFrameworkCore;
using System;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Repository.EFSQlite.DataBase.Mapping;

namespace TypiconOnline.Repository.EFSQLite.DataBase
{
    public class SQLiteDBContext : DbContext
    {
        private string _databasePath { get; set; }

        public SQLiteDBContext() 
        {
            _databasePath = @"Data\SQLiteDB.db";
        }

        public SQLiteDBContext(string connection)
        {
            _databasePath = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
            //для теста
            optionsBuilder.EnableSensitiveDataLogging();
        }

        #region Modeling

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // настройка полей с помощью Fluent API

            modelBuilder.ApplyConfiguration(new TypiconEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TypiconSettingsConfiguration());

            //modelBuilder.ApplyConfiguration(new TypiconRuleConfiguration());

            modelBuilder.ApplyConfiguration(new SignConfiguration());

            modelBuilder.ApplyConfiguration(new ModifiedYearConfiguration());

            //modelBuilder.ApplyConfiguration(new ModifiedRuleConfiguration());

            modelBuilder.ApplyConfiguration(new CommonRuleConfiguration());

            modelBuilder.ApplyConfiguration(new DayRuleConfiguration()); 
            modelBuilder.ApplyConfiguration(new DayWorshipConfiguration());
            modelBuilder.ApplyConfiguration(new DayRuleWorshipConfiguration());
            modelBuilder.ApplyConfiguration(new MenologyRuleConfiguration());
            modelBuilder.ApplyConfiguration(new TriodionRuleConfiguration());

            modelBuilder.ApplyConfiguration(new MenologyDayConfiguration());
            modelBuilder.ApplyConfiguration(new TriodionDayConfiguration());

            modelBuilder.ApplyConfiguration(new DayConfiguration());

            modelBuilder.ApplyConfiguration(new EasterItemConfiguration());

            modelBuilder.ApplyConfiguration(new ItemDateConfiguration());

            //modelBuilder.ApplyConfiguration(new ItemTextConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemFakeTextConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemTextStyledConfiguration()); 

            modelBuilder.Entity<OktoikhDay>();
            modelBuilder.Entity<TheotokionApp>();
            modelBuilder.Entity<Katavasia>();

            modelBuilder.ApplyConfiguration(new KathismaConfiguration()); 
            modelBuilder.ApplyConfiguration(new SlavaElementConfiguration());
            modelBuilder.ApplyConfiguration(new PsalmLinkConfiguration());
            modelBuilder.ApplyConfiguration(new PsalmConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
