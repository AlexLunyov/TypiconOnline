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
    //class ItemTextConfiguration : IEntityTypeConfiguration<ItemText>
    //{
    //    public void Configure(EntityTypeBuilder<ItemText> builder)
    //    {
    //        //builder.HasBaseType((Type)null);
    //        builder.Property<int>("Id");
    //        builder.Property(c => c.StringExpression);
    //        //builder.Ignore(c => c.IsEmpty);
    //        //builder.Ignore(c => c.IsTextEmpty);
    //        //builder.Ignore(c => c.IsValid);
    //        //builder.Ignore(c => c.Text);
    //    }
    //}

    //class ItemTextStyledConfiguration : IEntityTypeConfiguration<ItemTextStyled>
    //{
    //    public void Configure(EntityTypeBuilder<ItemTextStyled> builder)
    //    {
    //        builder.Ignore(c => c.Style);
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
