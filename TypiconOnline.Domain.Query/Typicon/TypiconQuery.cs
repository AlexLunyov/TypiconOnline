﻿using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconQuery: IDataQuery<TypiconEntityDTO>
    {
        public TypiconQuery(int typiconId)
        {
            TypiconId = typiconId;
        }

        public int TypiconId { get; }
    }
}
