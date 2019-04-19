using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Interfaces
{
    public interface ITypiconVersionChild: IHasId<int>
    {
        /// <summary>
        /// Id Устава (TypiconVersion)
        /// </summary>
        int TypiconVersionId { get; set; }

        TypiconVersion TypiconVersion { get; set; }
    }
}
