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
    /// Возвращает все Тексты минейных служб по заданной дате
    /// </summary>
    public class MenologyDayWorshipQueryHandler : DbContextQueryBase, IDataQueryHandler<MenologyDayWorshipQuery, Result<IQueryable<MenologyDayWorshipModel>>>
    {
        public MenologyDayWorshipQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<MenologyDayWorshipModel>> Handle([NotNull] MenologyDayWorshipQuery query)
        {
            ItemDate date = (query.Date != null)
                                ? new ItemDate(query.Date.Value.Month, query.Date.Value.Day)
                                : new ItemDate();

            var days = DbContext.Set<MenologyDay>()
                            .Include(c => c.DayWorships)
                                .ThenInclude(c => c.WorshipName)
                                    .ThenInclude(c => c.Items)
                            .Include(c => c.DayWorships)
                                .ThenInclude(c => c.WorshipShortName)
                                    .ThenInclude(c => c.Items)
                            .Where(c => c.LeapDate.Month == date.Month && c.LeapDate.Day == date.Day)
                            .ToList();

            var result = days.SelectMany(day => day.DayWorships,
                            (day, worship) => new MenologyDayWorshipModel()
                            {
                                WorshipId = worship.Id,
                                Date = (day.Date != null) ? day.Date.ToString() : string.Empty,
                                LeapDate = (day.LeapDate != null) ? day.LeapDate.ToString() : string.Empty,
                                IsCelebrating = worship.IsCelebrating,
                                Name = worship.WorshipName.ToString(query.Language),
                                ShortName = worship.WorshipShortName.ToString(query.Language),
                                UseFullName = worship.UseFullName
                            });

            return Result.Ok(result.AsQueryable());
        }

        
    }
}
