using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypiconOnline.Domain.Identity;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(r => r.Name).HasMaxLength(255);
        }
    }
}
