using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.Interfaces;

namespace TypiconOnline.Domain.AuthorizeKeys
{
    /// <summary>
    /// Ключ, дающий возможность редактировать OutputDay
    /// </summary>
    public class OutputDayCanEditKey: IAuthorizeKey
    {
        public OutputDayCanEditKey(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
