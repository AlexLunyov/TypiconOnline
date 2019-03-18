using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Implementations.Extensions
{
    public static class MenologyRuleExtensions
    {
        public static MenologyRule GetMenologyRule(this IEnumerable<MenologyRule> menologyRules, DateTime date)
        {
            return menologyRules.FirstOrDefault(c => c.GetCurrentDate(date.Year).Date == date.Date);
        }

        public static IEnumerable<MenologyRule> GetAllMovableRules(this IEnumerable<MenologyRule> menologyRules)
        {
            return menologyRules.Where(c => c.Date.IsEmpty && c.LeapDate.IsEmpty);
        }
    }
}
