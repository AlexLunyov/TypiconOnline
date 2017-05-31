using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF.DataBase.Mapping.Days
{
    internal class DayMap : EntityTypeConfiguration<Day>
    {
        public DayMap()
        {
            Property(c => c.Name).HasMaxLength(200).IsRequired();

            Property(c => c.Name1.StringExpression).IsRequired();

            Property(c => c.Name2.StringExpression).IsRequired();

            HasOptional(e => e.Folder).
                WithMany();
        }
    }
}
