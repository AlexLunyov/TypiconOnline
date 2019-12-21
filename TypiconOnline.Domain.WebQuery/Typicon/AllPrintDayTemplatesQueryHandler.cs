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
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает все Печатные шаблоны дней у черновика Сущности Устава
    /// </summary>
    public class AllPrintDayTemplatesQueryHandler : DbContextQueryBase, IQueryHandler<AllPrintDayTemplatesQuery, Result<IQueryable<PrintDayTemplateGridModel>>>
    {
        public AllPrintDayTemplatesQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<PrintDayTemplateGridModel>> Handle([NotNull] AllPrintDayTemplatesQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<PrintDayTemplateGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var entities = DbContext.Set<PrintDayTemplate>()
                .Where(c => c.TypiconVersionId == draft.Id);

            var result = entities.Select(c => new PrintDayTemplateGridModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Number = c.Number,
                    Icon = c.Icon,
                    HasFile = (c.PrintFile != null && c.PrintFile.Length > 0),
                    Deletable = c.SignLinks.Count == 0
                             && c.SignPrintLinks.Count == 0
                             && c.MenologyPrintLinks.Count == 0
                             && c.TriodionPrintLinks.Count == 0
            });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();


            return Result.Ok(result);
        }
    }
}
