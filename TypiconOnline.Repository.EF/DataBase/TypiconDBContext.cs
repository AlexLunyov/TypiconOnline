using System;
using System.Data.Entity;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Repository.EF.DataBase.Mapping;

namespace TypiconOnline.Repository.EF.DataBase
{
    public class TypiconDBContext : DbContext
    {
        public TypiconDBContext() : base("DBTypicon")
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<TypiconDBContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TypiconDBContext>());

            //убираем ленивую загрузку
            //Configuration.LazyLoadingEnabled = false;
        }

        #region Modeling

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // настройка полей с помощью Fluent API

            //modelBuilder.Configurations.Add(new FolderEntityMap());

            //modelBuilder.Configurations.Add(new TypiconFolderEntityMap());

            modelBuilder.Configurations.Add(new TypiconEntityMap());

            //modelBuilder.Configurations.Add(new RuleEntityMap());

            //modelBuilder.Configurations.Add(new DayMap());
            modelBuilder.Configurations.Add(new DayServiceMap());

            modelBuilder.Configurations.Add(new MenologyDayMap());
            modelBuilder.Configurations.Add(new DayRuleMap());

            modelBuilder.Configurations.Add(new MenologyRuleMap());

            ////modelBuilder.Configurations.Add(new MenologyFolderMap());

            modelBuilder.Configurations.Add(new TriodionDayMap());

            ////modelBuilder.Configurations.Add(new TriodionFolderMap());

            modelBuilder.Configurations.Add(new TriodionRuleMap());

            modelBuilder.Configurations.Add(new SignMap());

            modelBuilder.Configurations.Add(new EasterItemMap());

            modelBuilder.Configurations.Add(new ModifiedYearMap());

            modelBuilder.Configurations.Add(new ModifiedRuleMap());

            //modelBuilder.Configurations.Add(new ModifiedMenologyRuleMap());

            //modelBuilder.Configurations.Add(new ModifiedTriodionRuleMap());

            modelBuilder.Configurations.Add(new CommonRuleMap());

            modelBuilder.Configurations.Add(new KathismaMap());
            modelBuilder.Configurations.Add(new SlavaElementMap());

            modelBuilder.Entity<PsalmLink>().HasKey(c => c.Id);
            modelBuilder.Entity<PsalmLink>().HasRequired(c => c.Psalm);

            modelBuilder.Entity<Psalm>().HasKey(c => c.Id);

            //modelBuilder.Configurations.Add(new TypiconRuleMap());

            //modelBuilder.Configurations.Add(new TypiconDayRuleMap()); 

            modelBuilder.Configurations.Add(new IrmologionTheotokionMap());
            modelBuilder.Configurations.Add(new OktoikhDayMap());
            modelBuilder.Configurations.Add(new KatavasiaMap()); 

        }

        #endregion
    }
}
