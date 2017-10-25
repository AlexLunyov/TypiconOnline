using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class TypiconRuleConfiguration : IEntityTypeConfiguration<TypiconRule>
    {
        public void Configure(EntityTypeBuilder<TypiconRule> builder)
        {
            builder.Ignore(c => c.Rule);

            //builder.HasOne(e => e.Owner).
            //    WithMany();

            //builder.HasOne(e => e.Template).
            //    WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
