using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class TriodionDayConfiguration : IEntityTypeConfiguration<TriodionDay>
    {
        public void Configure(EntityTypeBuilder<TriodionDay> builder)
        {
            builder.ToTable("TriodionDays");
        }
    }
}
