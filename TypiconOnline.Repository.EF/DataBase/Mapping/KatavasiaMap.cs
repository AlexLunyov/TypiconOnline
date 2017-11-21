using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.TheotokionApp;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class KatavasiaMap : EntityTypeConfiguration<Katavasia>
    {
        public KatavasiaMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();
        }
    }
}
