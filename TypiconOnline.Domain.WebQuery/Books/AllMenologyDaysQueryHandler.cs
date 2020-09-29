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
using TypiconOnline.Domain.WebQuery.Context;
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
        private readonly WebDbContext _webDbContext;
        public AllMenologyDaysQueryHandler(TypiconDBContext dbContext, WebDbContext webDbContext) : base(dbContext)
        {
            _webDbContext = webDbContext ?? throw new ArgumentNullException(nameof(webDbContext));
        }

        public Result<IQueryable<MenologyDayGridModel>> Handle([NotNull] AllMenologyDaysQuery query)
        {
            var result = _webDbContext.MenologyDays;

            return Result.Ok(result.AsQueryable());
        }
    }
}
