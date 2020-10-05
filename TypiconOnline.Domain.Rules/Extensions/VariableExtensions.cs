using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Variables;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class VariableExtensions
    {
        public static string TryGetValue<T>(this Variable<T> variable, int typiconVersionId, IQueryProcessor queryProccesor) where T : class
        {
            var query = queryProccesor.Process(new TypiconVariableQuery(typiconVersionId, variable.VariableName));

            //если найдена и у нее определено Значение

            return query.Value?.Value ?? string.Empty;
        }
    }
}
