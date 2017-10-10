using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Books.Easter;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class EasterItemMap : EntityTypeConfiguration<EasterItem>
    {
        public EasterItemMap()
        {
            HasKey<DateTime>(c => c.Date);
            Property(c => c.Date).IsRequired();
            ToTable("Easters");
        }
    }
}
