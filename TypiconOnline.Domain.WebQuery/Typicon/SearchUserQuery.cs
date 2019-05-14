using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class SearchUserQuery : IQuery<IEnumerable<SearchUserModel>>
    {
        public SearchUserQuery(string search)
        {
            Search = search;
        }

        public string Search { get; }
    }
}
