using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    class DayWorshipMap : EntityTypeConfiguration<DayWorship>
    {
        public DayWorshipMap()
        {
            Property(c => c.WorshipName.StringExpression).IsRequired();


            HasRequired(e => e.Parent).
                WithMany();
            ToTable("DayServices");
        }
    }
}
