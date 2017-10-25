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
    class ItemTextConfiguration : IEntityTypeConfiguration<ItemText>
    {
        public void Configure(EntityTypeBuilder<ItemText> builder)
        {
            builder.Property<int>("Id");
            builder.Ignore(c => c.IsEmpty);
            builder.Ignore(c => c.IsTextEmpty);
            builder.Ignore(c => c.IsValid);
            builder.Ignore(c => c.Text);
        }
    }
}
