using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Identity;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class UserTypiconConfiguration : IEntityTypeConfiguration<UserTypicon>
    {
        public void Configure(EntityTypeBuilder<UserTypicon> builder)
        {
            builder.HasKey(c => new { c.UserId, c.TypiconId });

            builder.HasOne(c => c.User)
                .WithMany(c => c.EditableUserTypicons)
                .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.Typicon)
                .WithMany(c => c.EditableUserTypicons)
                .HasForeignKey(c => c.TypiconId);
        }
    }
}
