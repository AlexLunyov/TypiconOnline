using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputDayEditQuery : IQuery<Result<OutputDayEditModel>>, IHasAuthorizedAccess//<OutputDayCanEditKey>
    {
        public OutputDayEditQuery(int id)
        {
            Id = id;
        }

        /// <summary>
        /// OutputDay Id
        /// </summary>
        public int Id { get; }

        public IAuthorizeKey Key => new OutputDayCanEditKey(Id);
    }
}
