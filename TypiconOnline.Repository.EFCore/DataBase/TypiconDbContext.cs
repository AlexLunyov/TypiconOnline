using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Books.WeekDayApp;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Repository.EFCore.DataBase.Mapping;

namespace TypiconOnline.Repository.EFCore.DataBase
{
    public class TypiconDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                        UserRole, IdentityUserLogin<int>,
                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        //public DbSet<User> Users { get; set; }
        //public DbSet<Typicon> Typicons { get; set; }
        //public DbSet<TypiconVersion> TypiconVersions { get; set; }
        //public DbSet<MenologyRule> MenologyRules { get; set; }
        //public DbSet<TriodionRule> TriodionRules { get; set; }

        public TypiconDBContext(DbContextOptions<TypiconDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Включаем ленивую загрузку всех связанных свойств
            optionsBuilder.UseLazyLoadingProxies();

            //Игнорируем warnings

            //optionsBuilder.ConfigureWarnings(warnings => warnings.Log(CoreEventId.DetachedLazyLoadingWarning));
        }

        #region Modeling

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // настройка полей с помощью Fluent API TypiconSettings

            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
            //modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            //modelBuilder.Entity<User>().Property(u => u.UserName).HasMaxLength(255);
            //modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(255);
            //modelBuilder.Entity<Role>().Property(r => r.Name).HasMaxLength(255);

            #region Identity

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasKey(r => r.Id);
                
                b.Property(u => u.UserName).HasMaxLength(127);
                b.Property(u => u.NormalizedUserName).HasMaxLength(127);
                b.Property(u => u.FullName).HasMaxLength(127);
                b.Property(u => u.Email).HasMaxLength(127);
                b.Property(u => u.NormalizedEmail).HasMaxLength(127);

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.ToTable("Roles");
                role.HasKey(r => r.Id);
                role.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                role.Property(u => u.Name).HasMaxLength(127);
                //role.Property(u => u.SystemName).HasMaxLength(127);
                role.Property(u => u.NormalizedName).HasMaxLength(127);

                // Each Role can have many entries in the UserRole join table
                role.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(roleClaim =>
            {
                roleClaim.ToTable("RoleClaims");
                roleClaim.HasKey(rc => rc.Id);
            });

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(r => new { r.UserId, r.RoleId });
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(c =>
            {
                c.ToTable("UserLogins");
                c.Property(m => m.LoginProvider).HasMaxLength(127);
                c.Property(m => m.ProviderKey).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<int>>(c =>
            {
                c.ToTable("UserTokens");
                c.Property(m => m.LoginProvider).HasMaxLength(127);
                c.Property(m => m.Name).HasMaxLength(127);
            });

            #endregion

            modelBuilder.ApplyConfiguration(new TypiconConfiguration());
            modelBuilder.ApplyConfiguration(new TypiconClaimConfiguration());
            modelBuilder.ApplyConfiguration(new TypiconVersionConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypiconConfiguration()); 

            modelBuilder.ApplyConfiguration(new SignConfiguration());


            modelBuilder.ApplyConfiguration(new ModifiedYearConfiguration());
            
            modelBuilder.ApplyConfiguration(new ModifiedRuleConfiguration());
            modelBuilder.ApplyConfiguration(new DayWorshipsFilterConfiguration());

            modelBuilder.ApplyConfiguration(new CommonRuleConfiguration());

            modelBuilder.ApplyConfiguration(new DayRuleConfiguration());
            modelBuilder.ApplyConfiguration(new DayWorshipConfiguration());
            modelBuilder.ApplyConfiguration(new DayRuleWorshipConfiguration());

            modelBuilder.ApplyConfiguration(new MenologyRuleConfiguration());
            modelBuilder.Entity<TriodionRule>();
            //modelBuilder.ApplyConfiguration(new TriodionRuleConfiguration());

            modelBuilder.ApplyConfiguration(new ExplicitAddRuleConfiguration());

            modelBuilder.ApplyConfiguration(new OutputDayConfiguration());
            modelBuilder.ApplyConfiguration(new OutputWorshipConfiguration());
            modelBuilder.ApplyConfiguration(new OutputFormDayWorshipConfiguration()); 

            #region Variables

            modelBuilder.Entity<TypiconVariable>();

            modelBuilder.Entity<VariableRuleLink<CommonRule>>(c =>
            {
                c.HasKey(d => new { d.VariableId, d.EntityId });
                c.ToTable("CommonRuleVariables");
            });

            modelBuilder.Entity<VariableModRuleLink<Sign>>(c =>
            {
                c.HasKey(d => new { d.VariableId, d.EntityId, d.DefinitionType });
                c.ToTable("SignVariables");
            });

            modelBuilder.Entity<VariableModRuleLink<MenologyRule>>(c =>
            {
                c.HasKey(d => new { d.VariableId, d.EntityId, d.DefinitionType });
                c.ToTable("MenologyRuleVariables");
            });

            modelBuilder.Entity<VariableModRuleLink<TriodionRule>>(c =>
            {
                c.HasKey(d => new { d.VariableId, d.EntityId, d.DefinitionType });
                c.ToTable("TriodionRuleVariables");
            });

            modelBuilder.Entity<VariableRuleLink<ExplicitAddRule>>(c =>
            {
                c.HasKey(d => new { d.VariableId, d.EntityId });
                c.ToTable("ExplicitAddRuleVariables");
            });

            #endregion

            modelBuilder.ApplyConfiguration(new PrintWeekTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new PrintDayTemplateConfiguration());

            #region PrintDayTemplate Links

            modelBuilder.Entity<PrintTemplateModRuleLink<Sign>>(c =>
            {
                c.HasKey(d => new { d.TemplateId, d.EntityId });
                c.ToTable("SignPrintLinks");
            });

            modelBuilder.Entity<PrintTemplateModRuleLink<MenologyRule>>(c =>
            {
                c.HasKey(d => new { d.TemplateId, d.EntityId });
                c.ToTable("MenologyRulePrintLinks");
            });

            modelBuilder.Entity<PrintTemplateModRuleLink<TriodionRule>>(c =>
            {
                c.HasKey(d => new { d.TemplateId, d.EntityId });
                c.ToTable("TriodionRulePrintLinks");
            });

            #endregion



            modelBuilder.ApplyConfiguration(new TypiconVersionErrorConfiguration());

            modelBuilder.ApplyConfiguration(new MenologyDayConfiguration());
            modelBuilder.ApplyConfiguration(new TriodionDayConfiguration());

            modelBuilder.ApplyConfiguration(new DayConfiguration());

            modelBuilder.ApplyConfiguration(new EasterItemConfiguration());

            //modelBuilder.ApplyConfiguration(new ItemDateConfiguration());

            //modelBuilder.ApplyConfiguration(new ItemTextConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemTextUnitConfiguration());
            //modelBuilder.ApplyConfiguration(new ItemTextStyledConfiguration());

            modelBuilder.Entity<OktoikhDay>();
            modelBuilder.Entity<TheotokionApp>();
            modelBuilder.Entity<Katavasia>();
            modelBuilder.Entity<WeekDayApp>();

            modelBuilder.ApplyConfiguration(new KathismaConfiguration()); 
            modelBuilder.ApplyConfiguration(new SlavaElementConfiguration());
            modelBuilder.ApplyConfiguration(new PsalmLinkConfiguration());
            modelBuilder.ApplyConfiguration(new PsalmConfiguration());

            
        }

        #endregion
    }
}
