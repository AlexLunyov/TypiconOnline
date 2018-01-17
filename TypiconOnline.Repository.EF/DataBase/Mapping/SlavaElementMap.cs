using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class SlavaElementMap : EntityTypeConfiguration<SlavaElement>
    {
        public SlavaElementMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            HasMany(c => c.PsalmLinks);
        }
    }
}
