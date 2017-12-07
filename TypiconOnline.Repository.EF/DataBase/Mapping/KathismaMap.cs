using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    internal class KathismaMap : EntityTypeConfiguration<Kathisma>
    {
        public KathismaMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();

            HasRequired(e => e.TypiconEntity).
                WithMany();

            HasMany(c => c.SlavaElements);//.WithRequired();
        }
    }
}
