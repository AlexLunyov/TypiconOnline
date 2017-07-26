using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class MenologyDayMap : EntityTypeConfiguration<MenologyDay>
    {
        public MenologyDayMap()
        {
            HasKey<int>(c => c.Id);
            Property(c => c.Id).IsRequired();
            //Property(c => c.Name).HasMaxLength(200);
            //HasOptional(e => e.Folder).
            //    WithMany();

            //Ignore(c => c.Rule);

            //Property(c => c.RuleDefinition).HasColumnType("xml");//("NVARCHAR");

            //Property(c => c.DayName.StringExpression).IsRequired();

            //Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("MenologyDays");
            //});

            Property(c => c.Date.Expression).
                HasColumnName("Date").
                HasMaxLength(7).
                IsOptional();

            Property(c => c.DateB.Expression).
                HasColumnName("DateB").
                HasMaxLength(7).
                IsOptional();

            ToTable("MenologyDays");
        }
    }
}
