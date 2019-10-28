using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
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
    /// Возвращает все Тексты Дней Октоиха
    /// </summary>
    public class AllOktoikhDaysQueryHandler : DbContextQueryBase, IQueryHandler<AllOktoikhDaysQuery, Result<IQueryable<OktoikhDayModel>>>
    {
        public AllOktoikhDaysQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<OktoikhDayModel>> Handle([NotNull] AllOktoikhDaysQuery query)
        {
            var entities = DbContext.Set<OktoikhDay>()
                .ToList();

            var info = new CultureInfo("ru-RU").DateTimeFormat;

            var result = entities.Select(c => new OktoikhDayModel()
            {
                Id = c.Id,
                Ihos = c.Ihos,
                DayOfWeek = info.DayNames[(int)c.DayOfWeek]
            });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();

            return Result.Ok(result.AsQueryable());
        }

        
    }
}
