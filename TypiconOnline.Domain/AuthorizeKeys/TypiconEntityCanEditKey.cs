using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.AuthorizeKeys
{
    /// <summary>
    /// Ключ, дающий возможность редактировать TypiconEntity
    /// </summary>
    public class TypiconEntityCanEditKey : IAuthorizeKey
    {
        public TypiconEntityCanEditKey(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
