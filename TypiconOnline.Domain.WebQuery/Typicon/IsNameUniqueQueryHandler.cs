using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Проеряет на уникальность системное имя Устава
    /// </summary>
    public class IsNameUniqueQueryHandler : DbContextQueryBase, IQueryHandler<IsNameUniqueQuery, Result>
    {
        public IsNameUniqueQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result Handle([NotNull] IsNameUniqueQuery query)
        {
            //ищем в Уставах
            var found = DbContext.Set<TypiconEntity>()
                            .Any(c => c.SystemName == query.SystemName);

            if (!found)
            {
                //и в заявках
                found = DbContext.Set<TypiconClaim>()
                            .Any(c => c.SystemName == query.SystemName);
            }

            return (found) ? Result.Fail("") : Result.Ok();
        }

        
    }
}
