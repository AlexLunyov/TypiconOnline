using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class ItemTextConfiguration : IEntityTypeConfiguration<ItemText>
    {
        public void Configure(EntityTypeBuilder<ItemText> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.HasMany(c => c.Items);
            //.WithOne()
            //.HasForeignKey("ItemTextId");
        }
    }

    class ItemTextUnitConfiguration : IEntityTypeConfiguration<ItemTextUnit>
    {
        public void Configure(EntityTypeBuilder<ItemTextUnit> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");
            //builder.HasMany(c => c.Items).with
        }
    }

    //class ItemTextStyledConfiguration : IEntityTypeConfiguration<ItemTextStyled>
    //{
    //    public void Configure(EntityTypeBuilder<ItemTextStyled> builder)
    //    {
    //        builder.Property<int>("Id");
    //        builder.HasKey("Id");
    //        //builder.HasBaseType((Type)null);
    //        //builder.Property<int>("Id");
    //        //builder.Property(c => c.StringExpression);
    //        //builder.Ignore(c => c.IsEmpty);
    //        //builder.Ignore(c => c.IsTextEmpty);
    //        //builder.Ignore(c => c.IsValid);
    //        //builder.Ignore(c => c.Text);
    //    }
    //}
}
