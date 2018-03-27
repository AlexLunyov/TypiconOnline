using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    public class DayWorshipsFilterConfiguration : IEntityTypeConfiguration<DayWorshipsFilter>
    {
        public void Configure(EntityTypeBuilder<DayWorshipsFilter> builder)
        {
            builder.Property<int>("Id");
            builder.Property(c => c.ExcludedItem);
            builder.Property(c => c.IncludedItem);
            builder.Property(c => c.IsCelebrating);
        }
    }
}
