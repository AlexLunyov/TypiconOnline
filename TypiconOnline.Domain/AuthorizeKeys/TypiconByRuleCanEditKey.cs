using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.AuthorizeKeys
{
    /// <summary>
    /// Ключ, дающий возможность редактировать TypiconEntity
    /// </summary>
    public class TypiconByRuleCanEditKey<T> : IAuthorizeKey where T: class, ITypiconVersionChild, new()
    {
        public TypiconByRuleCanEditKey(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
