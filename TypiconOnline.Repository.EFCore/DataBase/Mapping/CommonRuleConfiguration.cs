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
    class CommonRuleConfiguration : IEntityTypeConfiguration<CommonRule>    
    {
        public void Configure(EntityTypeBuilder<CommonRule> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
