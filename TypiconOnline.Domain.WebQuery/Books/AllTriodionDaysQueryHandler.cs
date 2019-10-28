using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Books
{
    /// <summary>
    /// Возвращает все Тексты Минейных служб
    /// </summary>
    public class AllTriodionDaysQueryHandler : DbContextQueryBase, IQueryHandler<AllTriodionDaysQuery, Result<IQueryable<TriodionDayModel>>>
    {
        public AllTriodionDaysQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<TriodionDayModel>> Handle([NotNull] AllTriodionDaysQuery query)
        {
            var entities = DbContext.Set<TriodionDay>()
                .Include(c => c.DayWorships)
                        .ThenInclude(c => c.WorshipName)
                            .ThenInclude(c => c.Items)
                .Include(c => c.DayWorships)
                        .ThenInclude(c => c.WorshipShortName)
                            .ThenInclude(c => c.Items)
                .ToList();

            var result = entities.SelectMany(q => q.DayWorships.Select(c => new TriodionDayModel()
            {
                Id = c.Id,
                DaysFromEaster = q.DaysFromEaster,
                Name = c.WorshipName.FirstOrDefault(query.Language).ToString(),
                ShortName =  (c.WorshipShortName?.IsEmpty == false) ? c.WorshipShortName.FirstOrDefault(query.Language).ToString() : string.Empty,
                IsCelebrating = c.IsCelebrating
            }));

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();

            return Result.Ok(result.AsQueryable());
        }

        
    }
}
