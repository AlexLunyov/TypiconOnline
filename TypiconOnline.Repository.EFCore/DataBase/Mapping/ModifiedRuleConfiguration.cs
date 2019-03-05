using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class ModifiedRuleConfiguration : IEntityTypeConfiguration<ModifiedRule>
    {
        public void Configure(EntityTypeBuilder<ModifiedRule> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(e => e.ShortName).WithMany();

            //builder.OwnsOne(c => c.ShortName, 
            //    k =>
            //    {
            //        k.Ignore(d => d.Items);
            //        k.Ignore(d => d.IsBold);
            //        k.Ignore(d => d.IsRed);
            //        k.Ignore(d => d.IsItalic);
            //    }).HasKey("Id");

            //builder.WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.DayRule).WithMany();//.OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(c => c.DayWorships);
        }
    }
}
