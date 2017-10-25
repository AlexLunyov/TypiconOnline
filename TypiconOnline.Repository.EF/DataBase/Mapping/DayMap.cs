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
    public class DayMap : EntityTypeConfiguration<Day>
    {
        public DayMap()
        {
            //Property(c => c.DayName.StringExpression).IsRequired();
            HasMany(e => e.DayWorships).
                WithRequired(m => m.Parent);
        }
    }
}
