using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Domain.Common;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Выходную форму службы
    /// </summary>
    public class OutputWorshipEditQueryHandler : DbContextQueryBase, IQueryHandler<OutputWorshipEditQuery, Result<OutputWorshipEditModel>>
    {
        public OutputWorshipEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<OutputWorshipEditModel> Handle([NotNull] OutputWorshipEditQuery query)
        {
            var entity = DbContext.Set<OutputWorship>().FirstOrDefault(c => c.Id == query.Id);

            if (entity == null)
            {
                return Result.Fail<OutputWorshipEditModel>(404, "Выходная форма дня не найдена");
            }

            return Result.Ok(new OutputWorshipEditModel()
            { 
                Id = entity.Id,
                Name = entity.Name.FirstOrDefault(CommonConstants.DefaultLanguage).Text,
                NameStyle = new TextStyle()
                {
                    IsBold = entity.Name.IsBold,
                    IsItalic = entity.Name.IsItalic,
                    IsRed = entity.Name.IsRed
                },
                AdditionalName = entity.AdditionalName?.FirstOrDefault(CommonConstants.DefaultLanguage)?.Text,
                AdditionalNameStyle = (entity.AdditionalName != null)
                    ? new TextStyle()
                    {
                        IsBold = entity.AdditionalName.IsBold,
                        IsItalic = entity.AdditionalName.IsItalic,
                        IsRed = entity.AdditionalName.IsRed
                    }
                    : default,
                Time = entity.Time
            });
        }
    }
}
