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
    class TriodionRuleConfiguration : IEntityTypeConfiguration<TriodionRule>
    {
        public void Configure(EntityTypeBuilder<TriodionRule> builder)
        {
            //builder.HasOne(e => e.Template).
            //    WithMany()
            //    .OnDelete(DeleteBehavior.SetNull)
            //    ;
        }
    }
}
