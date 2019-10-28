using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Books
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class OktoikhDayEditQueryHandler : DbContextQueryBase, IQueryHandler<OktoikhDayEditQuery, Result<OktoikhDayEditModel>>
    {
        private const int LEAP_YEAR = 2016;

        public OktoikhDayEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<OktoikhDayEditModel> Handle([NotNull] OktoikhDayEditQuery query)
        {
            var found = DbContext.Set<OktoikhDay>()
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                var model = new OktoikhDayEditModel()
                {
                    Id = found.Id,
                    Ihos = found.Ihos,
                    DayOfWeek = new CultureInfo("ru-RU").DateTimeFormat.DayNames[(int)found.DayOfWeek],
                    Definition = found.Definition
                };

                return Result.Ok(model);
            }
            else
            {
                return Result.Fail<OktoikhDayEditModel>($"Текст службы Октоиха с Id={query.Id} не найден.");
            }
        }
    }
}
