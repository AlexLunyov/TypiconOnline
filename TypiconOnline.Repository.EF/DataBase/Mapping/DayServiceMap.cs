using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    class DayServiceMap : EntityTypeConfiguration<DayService>
    {
        public DayServiceMap()
        {
            Property(c => c.ServiceName.StringExpression).IsRequired();

            HasRequired(e => e.Parent).
                WithMany();
        }
    }
}
