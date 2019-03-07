using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            

        }
    }
}
