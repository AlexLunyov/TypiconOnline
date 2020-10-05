using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.AuthorizeKeys;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class VariableEditQuery : IQuery<Result<VariableEditModelBase>>, IHasAuthorizedAccess
    {
        public VariableEditQuery(int id)
        {
            Id = id;
            Key = new TypiconByRuleCanEditKey<TypiconVariable>(id);
        }

        public int Id { get; }

        public IAuthorizeKey Key { get; }
    }
}
