﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Jobs.Validation
{
    /// <summary>
    /// Проводит валидацию MenologyRule
    /// </summary>
    public class ValidateSignJobHandler : ValidateRuleJobHandler<Sign>, ICommandHandler<ValidateSignJob>
    {
        public ValidateSignJobHandler(TypiconDBContext dbContext, IJobRepository jobs, IRuleSerializerRoot serializer) 
            : base(dbContext, jobs, serializer)
        {
        }

        public async Task<Result> ExecuteAsync(ValidateSignJob job)
        {
            return await Handle(job, ErrorConstants.Sign);
        }
    }
}
