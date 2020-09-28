using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class ScheduleSettingsConfiguration : IEntityTypeConfiguration<ScheduleSettings>
    {
        const string SEPARATOR = ",";

        public void Configure(EntityTypeBuilder<ScheduleSettings> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Ignore(c => c.DayWorships);

            var converter = new ValueConverter<List<DateTime>, string>(
                v => string.Join(SEPARATOR, v.Select(c => c.ToBinary())),
                v =>
                    (!string.IsNullOrEmpty(v)) ?
                        v.Split(SEPARATOR, StringSplitOptions.None)
                          .Select(c => DateTime.FromBinary(long.Parse(c)))
                          .ToList()
                        : new List<DateTime>()
                );

            builder.Property(c => c.IncludedDates)
                .HasConversion(converter);

            builder.Property(c => c.ExcludedDates)
                .HasConversion(converter);

        }
    }
}
