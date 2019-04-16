using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public static class BusinessConstraintExtensions
    {
        public static string GetSummary(this IReadOnlyCollection<BusinessConstraint> items)
        {
            var builder = new StringBuilder();

            foreach (var item in items)
            {
                builder.AppendLine(item.ConstraintFullDescription);
            }

            return builder.ToString();
        }
    }
}
