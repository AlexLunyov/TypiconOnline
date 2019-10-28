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
    public class TriodionDayEditQueryHandler : DbContextQueryBase, IQueryHandler<TriodionDayEditQuery, Result<TriodionDayEditModel>>
    {
        private const int LEAP_YEAR = 2016;

        public TriodionDayEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<TriodionDayEditModel> Handle([NotNull] TriodionDayEditQuery query)
        {
            var found = DbContext.Set<DayWorship>()
                .Include(c => c.Parent)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                var model = new TriodionDayEditModel()
                {
                    Id = found.Id,
                    DaysFromEaster = (found.Parent as TriodionDay).DaysFromEaster,
                    Name = found.WorshipName,
                    ShortName = found.WorshipShortName,
                    IsCelebrating = found.IsCelebrating,
                    UseFullName = found.UseFullName,
                    Definition = found.Definition
                };

                return Result.Ok(model);
            }
            else
            {
                return Result.Fail<TriodionDayEditModel>($"Текст службы Троиди с Id={query.Id} не найден.");
            }
        }
    }
}
