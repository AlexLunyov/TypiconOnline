using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает все Тексты триодных служб по заданному числу дней от Пасхи
    /// </summary>
    public class TriodionDayWorshipQueryHandler : DbContextQueryBase, IQueryHandler<TriodionDayWorshipQuery, Result<IQueryable<TriodionDayWorshipModel>>>
    {
        public TriodionDayWorshipQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<IQueryable<TriodionDayWorshipModel>> Handle([NotNull] TriodionDayWorshipQuery query)
        {
            var days = DbContext.Set<TriodionDay>()
                            .Include(c => c.DayWorships)
                                .ThenInclude(c => c.WorshipName)
                                    .ThenInclude(c => c.Items)
                            .Include(c => c.DayWorships)
                                .ThenInclude(c => c.WorshipShortName)
                                    .ThenInclude(c => c.Items)
                            .Where(c => c.DaysFromEaster == query.DaysFromEaster)
                            .ToList();

            var result = days.SelectMany(day => day.DayWorships,
                            (day, worship) => new TriodionDayWorshipModel()
                            {
                                WorshipId = worship.Id,
                                DaysFromEaster = day.DaysFromEaster,
                                IsCelebrating = worship.IsCelebrating,
                                Name = worship.WorshipName.ToString(query.Language),
                                ShortName = worship.WorshipShortName.ToString(query.Language),
                                UseFullName = worship.UseFullName
                            });

            return Result.Ok(result.AsQueryable());
        }
    }
}
