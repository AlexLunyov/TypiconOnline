using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping.Typicon
{
    public class TypiconRuleMap : EntityTypeConfiguration<TypiconRule>
    {
        public TypiconRuleMap()
        {
            HasOptional(e => e.Template).
                WithMany();
        }
    }
}
