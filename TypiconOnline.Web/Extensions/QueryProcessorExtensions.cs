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
        public static IEnumerable<SelectListItem> GetTypicons(this IQueryProcessor queryProcessor)
        {
            var typicons = queryProcessor.Process(new AllTypiconsQuery());

            return typicons.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
        }
        
        public static IEnumerable<SelectListItem> GetSigns(this IQueryProcessor queryProcessor, int typiconId, string language, int? exceptSignId = null)
        {
            var signs = queryProcessor.Process(new AllSignsQuery(typiconId, language, exceptSignId));

            if (signs.Success)
            {
                return signs.Value.Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            }
            else
            {
                return new List<SelectListItem>();
            }
        }

        public static IEnumerable<SelectListItem> GetPrintTemplates(this IQueryProcessor queryProcessor, int typiconId)
        {
            var signs = queryProcessor.Process(new AllPrintDayTemplatesQuery(typiconId));

            if (signs.Success)
            {
                return signs.Value.Select(c => new SelectListItem() { Text = $"{c.Number} ({c.Name})", Value = c.Id.ToString() });
            }
            else
            {
                return new List<SelectListItem>();
            }
        }
    }
}
