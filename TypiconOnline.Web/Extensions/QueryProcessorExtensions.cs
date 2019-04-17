using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Web.Extensions
{
    public static class QueryProcessorExtensions
    {
        public static IEnumerable<SelectListItem> GetTypicons(this IDataQueryProcessor queryProcessor)
        {
            var typicons = queryProcessor.Process(new AllTypiconsQuery());

            return typicons.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
        }
    }
}
