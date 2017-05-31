using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping.Typicon
{
    public class CommonRuleMap : EntityTypeConfiguration<CommonRule>
    {
        public CommonRuleMap()
        {
            Property(c => c.Name).HasMaxLength(200).IsRequired();

            HasRequired(e => e.Folder).
                WithMany();

            HasRequired(e => e.Owner).
                WithMany();
        }
    }
}
