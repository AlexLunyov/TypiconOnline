using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query
{
    public abstract class UnitOfWorkHandlerBase
    {
        protected IUnitOfWork UnitOfWork { get; }

        public UnitOfWorkHandlerBase([NotNull] IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
