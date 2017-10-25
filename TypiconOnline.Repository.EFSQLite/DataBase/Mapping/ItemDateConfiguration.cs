using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class ItemDateConfiguration : IEntityTypeConfiguration<ItemDate>
    {
        public void Configure(EntityTypeBuilder<ItemDate> builder)
        {
            builder.Property<int>("Id");
            builder.Ignore(c => c.Day);
            builder.Ignore(c => c.Month);
        }
    }
}
