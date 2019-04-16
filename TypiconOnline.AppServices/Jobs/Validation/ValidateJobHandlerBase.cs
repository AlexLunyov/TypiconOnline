using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    public abstract class ValidateJobHandlerBase
    {
        public ValidateJobHandlerBase(TypiconDBContext dbContext
            , IJobRepository jobs
            , IRuleSerializerRoot serializer)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Jobs = jobs ?? throw new ArgumentNullException(nameof(jobs));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        protected TypiconDBContext DbContext { get; }
        protected IJobRepository Jobs { get; }
        protected IRuleSerializerRoot Serializer { get; }
    }
}
