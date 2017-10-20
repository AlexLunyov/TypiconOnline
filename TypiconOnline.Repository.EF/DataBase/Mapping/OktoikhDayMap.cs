using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    class OktoikhDayMap : EntityTypeConfiguration<OktoikhDay>
    {
        public OktoikhDayMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();
        }
    }
}
