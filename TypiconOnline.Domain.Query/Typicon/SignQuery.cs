﻿using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class SignQuery : IDataQuery<Sign>
    {
        public SignQuery(int signId)
        {
            SignId = signId;
        }

        public int SignId { get; }
    }
}