using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class TriodionDayMap : EntityTypeConfiguration<TriodionDay>
    {
        public TriodionDayMap()
        {
            //    HasKey<int>(c => c.Id);

            //    Property(c => c.Id).IsRequired();

            //Property(c => c.Name).HasMaxLength(200);

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("TriodionDay");
            //});

            Property(c => c.DaysFromEaster).IsRequired();

            //Ignore(c => c.Rule);

            //Property(c => c.RuleDefinition).HasColumnType("NVARCHAR");

            ToTable("TriodionDays");
        }
    }
}
