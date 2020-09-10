﻿using JetBrains.Annotations;
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
using TypiconOnline.Domain.WebQuery.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Выходную форму дня для недели
    /// </summary>
    public class OutputDayEditQueryHandler : DbContextQueryBase, IQueryHandler<OutputDayEditQuery, Result<OutputDayEditModel>>
    {
        public OutputDayEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<OutputDayEditModel> Handle([NotNull] OutputDayEditQuery query)
        {
            var entity = DbContext.Set<OutputDay>().FirstOrDefault(c => c.Id == query.Id);

            if (entity == null)
            {
                return Result.Fail<OutputDayEditModel>(404, "Выходная форма дня не найдена");
            }

            return Result.Ok(new OutputDayEditModel()
            { 
                Id = entity.Id,
                Name = new ItemTextStyled(entity.Name)
            });
        }
    }
}
