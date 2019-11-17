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
    public class AllMenologyDaysQueryHandler : DbContextQueryBase, IQueryHandler<AllMenologyDaysQuery, Result<IQueryable<MenologyDayGridModel>>>
    {
        public AllMenologyDaysQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<MenologyDayGridModel>> Handle([NotNull] AllMenologyDaysQuery query)
        {
            var entities = DbContext.Set<MenologyDay>()
                .Include(c => c.DayWorships)
                        .ThenInclude(c => c.WorshipName)
                            .ThenInclude(c => c.Items)
                .Include(c => c.DayWorships)
                        .ThenInclude(c => c.WorshipShortName)
                            .ThenInclude(c => c.Items)
                .ToList();

            var result = entities.SelectMany(q => q.DayWorships.Select(c => new MenologyDayGridModel()
            {
                Id = c.Id,
                Date = (!q.Date.IsEmpty) ? q.Date.ToString() : string.Empty,
                LeapDate = (!q.LeapDate.IsEmpty) ? q.LeapDate.ToString() : string.Empty,
                Name = c.WorshipName.FirstOrDefault(query.Language).ToString(),

                ShortName =  (c.WorshipShortName?.IsEmpty == false) ? c.WorshipShortName.FirstOrDefault(query.Language).ToString() : string.Empty,
                IsCelebrating = c.IsCelebrating
            }));

            //var list = new List<MenologyDayModel>();

            //foreach (var entity in entities)
            //{
            //    var toAdd = entity.DayWorships.Select(c => new MenologyDayModel()
            //    {
            //        Id = c.Id,
            //        Date = (!entity.Date.IsEmpty) ? entity.Date.ToString() : string.Empty,
            //        LeapDate = (!entity.LeapDate.IsEmpty) ? entity.LeapDate.ToString() : string.Empty,
            //        Name = c.WorshipName.FirstOrDefault(query.Language).ToString(),

            //        ShortName = (c.WorshipShortName?.IsEmpty == false) ? c.WorshipShortName.FirstOrDefault(query.Language).ToString() : string.Empty,
            //        IsCelebrating = c.IsCelebrating
            //    });

            //    list.AddRange(toAdd);
            //}

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();

            return Result.Ok(result.AsQueryable());
        }

        
    }
}
