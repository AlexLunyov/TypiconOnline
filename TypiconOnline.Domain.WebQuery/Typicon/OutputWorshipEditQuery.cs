using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class OutputWorshipEditQuery : IQuery<Result<OutputWorshipEditModel>>, IHasAuthorizedAccess//<OutputDayCanEditKey>
    {
        public OutputWorshipEditQuery(int id)
        {
            Id = id;
        }

        /// <summary>
        /// OutputDay Id
        /// </summary>
        public int Id { get; }

        public IAuthorizeKey Key => new OutputWorshipCanEditKey(Id);
    }
}
