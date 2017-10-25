using Microsoft.EntityFrameworkCore;
using System;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFSQlite.DataBase.Mapping;

namespace TypiconOnline.Repository.EFSQLite.DataBase
{
    public class SQLiteDBContext : DbContext
    {
        private string _databasePath { get; set; }

        public SQLiteDBContext() 
        {
            _databasePath = "Filename=SQLiteDB.db";
        }

        public SQLiteDBContext(string connection)
        {
            _databasePath = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }

        #region Modeling

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // настройка полей с помощью Fluent API

            modelBuilder.ApplyConfiguration(new TypiconEntityConfiguration());

            //modelBuilder.ApplyConfiguration(new TypiconRuleConfiguration());

            modelBuilder.ApplyConfiguration(new SignConfiguration());

            modelBuilder.ApplyConfiguration(new ModifiedYearConfiguration());

            //modelBuilder.ApplyConfiguration(new ModifiedRuleConfiguration());

            modelBuilder.ApplyConfiguration(new CommonRuleConfiguration());

            modelBuilder.ApplyConfiguration(new DayRuleConfiguration());
            modelBuilder.ApplyConfiguration(new DayWorshipConfiguration());
            modelBuilder.ApplyConfiguration(new MenologyRuleConfiguration());
            //modelBuilder.Configurations.Add(new TriodionRuleMap());

            modelBuilder.ApplyConfiguration(new MenologyDayConfiguration());
            modelBuilder.ApplyConfiguration(new TriodionDayConfiguration());

            modelBuilder.ApplyConfiguration(new DayConfiguration());

            modelBuilder.ApplyConfiguration(new EasterItemConfiguration());

            modelBuilder.ApplyConfiguration(new ItemDateConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTextConfiguration());

            

            modelBuilder.Entity<OktoikhDay>();
            modelBuilder.Entity<TheotokionApp>();


            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
