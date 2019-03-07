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
    class MenologyRuleConfiguration : IEntityTypeConfiguration<MenologyRule>
    {
        public void Configure(EntityTypeBuilder<MenologyRule> builder)
        {
            builder.OwnsOne(c => c.Date)
                .Ignore(c => c.Expression);
            builder.OwnsOne(c => c.LeapDate)
                .Ignore(c => c.Expression);
        }
    }
}
