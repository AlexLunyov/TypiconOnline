using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class EasterItemConfiguration : IEntityTypeConfiguration<EasterItem>
    {
        public void Configure(EntityTypeBuilder<EasterItem> builder)
        {
            builder.HasKey(c => c.Date);
        }
    }
}
