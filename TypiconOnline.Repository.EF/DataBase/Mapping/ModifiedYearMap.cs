using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class ModifiedYearMap : EntityTypeConfiguration<ModifiedYear>
    {
        public ModifiedYearMap()
        {
            HasKey(u => new { u.Id, u.TypiconEntityId });

            Property(c => c.Year).IsRequired();

            HasRequired(c => c.TypiconEntity).
                WithMany(c => c.ModifiedYears).
                HasForeignKey(x => x.TypiconEntityId); 

            HasMany(c => c.ModifiedRules);

            ToTable("ModifiedYears");
        }
    }
}
