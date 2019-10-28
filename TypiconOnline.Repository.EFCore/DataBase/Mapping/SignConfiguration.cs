using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class SignConfiguration : IEntityTypeConfiguration<Sign>
    {
        public void Configure(EntityTypeBuilder<Sign> builder)
        {
            builder.HasBaseType((Type)null);

            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.SignName, name =>
            {
                name.OwnsMany(c => c.Items, items =>
                {
                    items.Property<int>("NameId");
                    items.WithOwner().HasForeignKey("NameId");
                    items.Property<int>("Id");
                    items.HasKey("Id");
                    items.ToTable("SignNameItems");
                });
                //.ToTable("SignName");
            });

            builder.HasOne(e => e.Template).
                WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.TemplateId)
                .IsRequired(false)
                ;

            //builder.Ignore(c => c.IsTemplate);
        }
    }
}
