using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EF.DataBase.Mapping
{
    public class DayRuleWorshipMap : EntityTypeConfiguration<DayRuleWorship>
    {
        public DayRuleWorshipMap()
        {
            HasKey(c => new { c.DayRuleId, c.DayWorshipId });
        }
    }
}
