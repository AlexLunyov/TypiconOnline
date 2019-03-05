using Microsoft.EntityFrameworkCore;
using System;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Repository.EFCore.DataBase.Mapping;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class TypiconDBContext : DbContext
    {
        public TypiconDBContext(DbContextOptions<TypiconDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Включаем ленивую загрузку всех связанных свойств
            optionsBuilder.UseLazyLoadingProxies();
        }

        #region Modeling

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // настройка полей с помощью Fluent API TypiconSettings

            modelBuilder.ApplyConfiguration(new TypiconEntityConfiguration());

            //modelBuilder.ApplyConfiguration(new TypiconRuleConfiguration());

            modelBuilder.ApplyConfiguration(new SignConfiguration());

            //Sign

            //modelBuilder
            //    .Entity<Sign>()
            //    .HasBaseType((Type)null);
            //modelBuilder
            //    .Entity<Sign>()
            //    .HasKey(c => c.Id);
            //modelBuilder
            //    .Entity<Sign>()
            //    .HasKey(c => c.Id);
            //modelBuilder
            //    .Entity<Sign>()
            //    .Property(c => c.Priority)
            //    .IsRequired();
            //modelBuilder
            //    .Entity<Sign>()
            //    .HasOne(e => e.SignName)
            //    .WithMany();
            //modelBuilder
            //    .Entity<Sign>()
            //    .HasOne(e => e.Template)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasForeignKey(c => c.TemplateId)
            //    .IsRequired(false);

            ////ItemText
            //modelBuilder.Entity<ItemText>()
            //    .Property<int>("Id");

            //modelBuilder.Entity<ItemText>()
            //    .HasKey("Id");

            //modelBuilder.Entity<ItemText>()
            //    .HasMany(c => c.Items);

            //modelBuilder.Entity<ItemTextUnit>()
            //    .Property<int>("Id");

            //modelBuilder.Entity<ItemTextUnit>()
            //    .HasKey("Id");


            modelBuilder.ApplyConfiguration(new ModifiedYearConfiguration());
            
            modelBuilder.ApplyConfiguration(new ModifiedRuleConfiguration());
            modelBuilder.ApplyConfiguration(new DayWorshipsFilterConfiguration());

            modelBuilder.ApplyConfiguration(new CommonRuleConfiguration());

            modelBuilder.ApplyConfiguration(new DayRuleConfiguration());
            modelBuilder.ApplyConfiguration(new DayWorshipConfiguration());
            modelBuilder.ApplyConfiguration(new DayRuleWorshipConfiguration());

            //modelBuilder.ApplyConfiguration(new MenologyRuleConfiguration());
            //modelBuilder.ApplyConfiguration(new TriodionRuleConfiguration());

            modelBuilder.ApplyConfiguration(new MenologyDayConfiguration());
            modelBuilder.ApplyConfiguration(new TriodionDayConfiguration());

            modelBuilder.ApplyConfiguration(new DayConfiguration());

            modelBuilder.ApplyConfiguration(new EasterItemConfiguration());

            modelBuilder.ApplyConfiguration(new ItemDateConfiguration());

            modelBuilder.ApplyConfiguration(new ItemTextConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTextUnitConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemTextStyledConfiguration());

            modelBuilder.Entity<OktoikhDay>();
            modelBuilder.Entity<TheotokionApp>();
            modelBuilder.Entity<Katavasia>();
            modelBuilder.Entity<WeekDayApp>();

            modelBuilder.ApplyConfiguration(new KathismaConfiguration()); 
            modelBuilder.ApplyConfiguration(new SlavaElementConfiguration());
            modelBuilder.ApplyConfiguration(new PsalmLinkConfiguration());
            modelBuilder.ApplyConfiguration(new PsalmConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
