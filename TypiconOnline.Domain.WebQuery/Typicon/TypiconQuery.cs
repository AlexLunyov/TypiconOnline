﻿using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.WebQuery.Typicon
{ 
    public class TypiconQuery : IQuery<Result<TypiconEntityModel>>
    {
        public TypiconQuery(int id)
        {
            Id = id;
        }

        public TypiconQuery(int id, string language) : this(id)
        {
            Language = language;
        }

        public int Id { get; }
        public string Language { get; } = "cs-ru";
    }
}
