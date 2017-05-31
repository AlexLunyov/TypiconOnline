using System;
using System.Data.Entity;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Easter;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
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
            Configuration.LazyLoadingEnabled = false;
        }

        #region Properties

        public DbSet<TypiconEntity> TypiconEntitySet { get; set; }

        public DbSet<MenologyDay> MenologyDaySet { get; set; }

        public DbSet<TriodionDay> TriodionDaySet { get; set; }

        public DbSet<MenologyRule> MenologyRuleSet { get; set; }

        public DbSet<TriodionRule> TriodionRuleSet { get; set; }

        //public DbSet<RuleEntity> RulesSet { get; set; }

        public DbSet<Sign> SignSet { get; set; }

        public DbSet<FolderEntity> FolderEntitySet { get; set; }

        public DbSet<EasterItem> EasterItemSet { get; set; }

        public DbSet<ModifiedYear> ModifiedYearSet { get; set; }

        public DbSet<ModifiedRule> ModifiedRuleSet { get; set; }

        //public DbSet<EasterStorage> EasterStorageSet { get; set; }

        //public DbSet<TypiconFolderEntity> TypiconFolderEntitySet { get; set; }

        //public DbSet<RuleFolderEntity<MenologyDay>> FolderMenologyEntitySet { get; set; }

        //public DbSet<RuleFolderEntity<TriodionDay>> FolderTriodionEntitySet { get; set; }

        //public DbSet<RuleFolderEntity<OktoikhDay>> FolderOktoikhEntitySet { get; set; }

        #endregion


        #region Modeling

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // настройка полей с помощью Fluent API

            modelBuilder.Configurations.Add(new FolderEntityMap());

            modelBuilder.Configurations.Add(new TypiconFolderEntityMap());

            modelBuilder.Configurations.Add(new TypiconEntityMap());

            modelBuilder.Configurations.Add(new RuleEntityMap());

            modelBuilder.Configurations.Add(new MenologyDayMap());

            modelBuilder.Configurations.Add(new MenologyRuleMap());

            //modelBuilder.Configurations.Add(new MenologyFolderMap());

            modelBuilder.Configurations.Add(new TriodionDayMap());

            //modelBuilder.Configurations.Add(new TriodionFolderMap());

            modelBuilder.Configurations.Add(new TriodionRuleMap());

            modelBuilder.Configurations.Add(new SignMap());

            modelBuilder.Configurations.Add(new EasterItemMap());

            modelBuilder.Configurations.Add(new ModifiedYearMap());

            modelBuilder.Configurations.Add(new ModifiedRuleMap());

            modelBuilder.Configurations.Add(new ModifiedMenologyRuleMap());

            modelBuilder.Configurations.Add(new ModifiedTriodionRuleMap());

            //modelBuilder.Configurations.Add(new TypiconRuleMap());
        }

        #endregion

        #region old
        //private void TypiconEntityCreate(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TypiconEntity>().HasKey<int>(c => c.Id);
        //    modelBuilder.Entity<TypiconEntity>()
        //        .Property(c => c.Id).IsRequired();

        //    modelBuilder.Entity<TypiconEntity>()
        //        .Property(c => c.Name).HasMaxLength(200);

        //    modelBuilder.Entity<TypiconEntity>().
        //        HasOptional(e => e.Template).
        //        WithMany()/*.
        //        HasForeignKey(m => m.TemplateId)*/;

        //    modelBuilder.Entity<TypiconEntity>().
        //        HasMany(e => e.Signs).
        //        WithRequired(m => m.TypiconEntity);

        //    modelBuilder.Entity<TypiconEntity>().ToTable("TypiconEntity");
        //}

        //private void MenologyDayCreate(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MenologyDay>().HasKey<int>(c => c.Id);
        //    modelBuilder.Entity<MenologyDay>()
        //        .Property(c => c.Id).IsRequired();

        //    modelBuilder.Entity<MenologyDay>()
        //        .Property(c => c.Name).HasMaxLength(200);

        //    modelBuilder.Entity<MenologyDay>()
        //        .Property(c => c.Date.Expression).
        //        HasColumnName("Date").
        //        HasMaxLength(7).
        //        IsOptional();

        //    modelBuilder.Entity<MenologyDay>()
        //        .Property(c => c.DateB.Expression).
        //        HasColumnName("BDate").
        //        HasMaxLength(7).
        //        IsOptional();

        //    modelBuilder.Entity<MenologyDay>().ToTable("MenologyDay");
        //}

        //private void SignCreate(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Sign>().HasKey<int>(c => c.Id);
        //    modelBuilder.Entity<Sign>()
        //        .Property(c => c.Id).IsRequired();

        //    modelBuilder.Entity<Sign>()
        //        .Property(c => c.SignNumber);

        //    modelBuilder.Entity<Sign>()
        //        .Property(c => c.Name).HasMaxLength(100);

        //    modelBuilder.Entity<Sign>()
        //        .Property(c => c.IsTemplate).;

        //    modelBuilder.Entity<Sign>().
        //        HasOptional(e => e.Template).
        //        WithMany()/*.
        //        HasForeignKey(m => m.TemplateId)*/;

        //    modelBuilder.Entity<Sign>().
        //        HasRequired(e => e.TypiconEntity).
        //        WithMany()/*.
        //        HasForeignKey(m => m.TemplateId)*/;

        //    modelBuilder.Entity<Sign>().ToTable("Sign");
        //}

        #endregion
    }
}
