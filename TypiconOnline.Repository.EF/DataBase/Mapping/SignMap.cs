using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class SignMap : EntityTypeConfiguration<Sign>
    {
        public SignMap()
        {
            //HasKey<int>(c => c.Id);
            //Property(c => c.Id).IsRequired();

            //Property(c => c.Name).HasMaxLength(100).IsRequired();

            Property(c => c.Number).IsRequired();

            Property(c => c.Priority).IsRequired();

            Ignore(c => c.IsTemplate);

            HasOptional(e => e.Template).
                WithMany().WillCascadeOnDelete(false);

            HasRequired(e => e.TypiconEntity).
                WithMany();

            //Ignore(c => c.Rule);

            //Property(c => c.RuleDefinition).HasColumnType("NVARCHAR");

            ToTable("Signs");
        }
    }
}
