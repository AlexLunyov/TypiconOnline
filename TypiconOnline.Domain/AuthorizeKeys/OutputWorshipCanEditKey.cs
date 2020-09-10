using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.AuthorizeKeys
{
    /// <summary>
    /// Ключ, дающий возможность редактировать OutputWorship
    /// </summary>
    public class OutputWorshipCanEditKey : IAuthorizeKey
    {
        public OutputWorshipCanEditKey(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
