using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
    public class MenologyDayEditQueryHandler : DbContextQueryBase, IQueryHandler<MenologyDayEditQuery, Result<MenologyDayEditModel>>
    {
        private const int LEAP_YEAR = 2016;

        public MenologyDayEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<MenologyDayEditModel> Handle([NotNull] MenologyDayEditQuery query)
        {
            var found = DbContext.Set<DayWorship>()
                .Include(c => c.Parent)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                var model = new MenologyDayEditModel()
                {
                    Id = found.Id,
                    Name = found.WorshipName,
                    ShortName = found.WorshipShortName,
                    IsCelebrating = found.IsCelebrating,
                    UseFullName = found.UseFullName,
                    Definition = found.Definition
                };

                var parent = found.Parent as MenologyDay;

                if (parent.LeapDate?.IsEmpty == false)
                {
                    model.LeapDate = new DateTime(LEAP_YEAR, parent.LeapDate.Month, parent.LeapDate.Day);
                }

                return Result.Ok(model);
            }
            else
            {
                return Result.Fail<MenologyDayEditModel>($"Текст Минейной службы с Id={query.Id} не найден.");
            }
        }
    }
}
