using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    class CommonRuleMap : EntityTypeConfiguration<CommonRule>
    {
        public CommonRuleMap()
        {
            HasRequired(e => e.Owner).
                WithMany();
            ToTable("CommonRules");
        }
    }
}
