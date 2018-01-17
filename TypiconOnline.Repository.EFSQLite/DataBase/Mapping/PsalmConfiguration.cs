using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Psalter;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class PsalmConfiguration : IEntityTypeConfiguration<Psalm>
    {
        public void Configure(EntityTypeBuilder<Psalm> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Definition);
            builder.Property(c => c.Number);
        }
    }
}
