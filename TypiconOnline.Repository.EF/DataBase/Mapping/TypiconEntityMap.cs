using System.Data.Entity.ModelConfiguration;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class TypiconEntityMap : EntityTypeConfiguration<TypiconEntity>
    {
        public TypiconEntityMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            Property(c => c.Name).HasMaxLength(200);

            HasOptional(e => e.Template).
                WithMany();

            //HasMany(e => e.Signs).
            //    WithRequired(m => m.Owner);

            HasMany(e => e.ModifiedYears).
                WithRequired(m => m.TypiconEntity);

            HasRequired(e => e.Settings).
                WithRequiredPrincipal();


            //HasRequired(e => e.MenologyRules).
            //    WithRequiredDependent(m => m.Owner);

            //HasRequired(e => e.TriodionRules).
            //    WithRequiredDependent(m => m.Owner);

            //HasRequired(e => e.OktoikhRules).
            //    WithRequiredDependent(m => m.Owner);

            HasRequired(e => e.RulesFolder).
                WithOptional(m => m.Owner).WillCascadeOnDelete(false);

            ToTable("TypiconEntities");
        }
    }
}
