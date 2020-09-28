using JetBrains.Annotations;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Output;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает фильтрованную Выходную форму для службы вместе с последовательностью
    /// </summary>
    public class OutputWorshipQueryHandler : DbContextQueryBase, IQueryHandler<OutputWorshipQuery, Result<FilteredOutputWorshipExtended>>
    {
        private readonly IJobRepository _jobs;
        private readonly ITypiconSerializer _typiconSerializer;

        public OutputWorshipQueryHandler(TypiconDBContext dbContext
            , ITypiconSerializer typiconSerializer)
            : base(dbContext)
        {
            _typiconSerializer = typiconSerializer ?? throw new ArgumentNullException(nameof(typiconSerializer));
        }

        public Result<FilteredOutputWorshipExtended> Handle([NotNull] OutputWorshipQuery query)
        {
            var worship = DbContext.Set<OutputWorship>().FirstOrDefault(c => c.Id == query.Id);

            //нашли 
            if (worship != null)
            {
                try
                {
                    var sections = _typiconSerializer.Deserialize<OutputSectionModelCollection>(worship.Definition);
                    
                    var w = new FilteredOutputWorshipExtended()
                    {
                        Id = worship.Id,
                        Time = worship.Time,
                        Name = worship.Name.FilterOut(query.Filter),
                        HasSequence = !string.IsNullOrEmpty(worship.Definition),
                        Sections = sections.FilterOut(query.Filter)
                };
                    return Result.Ok(w);
                }
                catch
                {
                    //ошибка десериализации
                    return Result.Fail<FilteredOutputWorshipExtended>($"Возникла ошибка с отображением последовательности богослужения.");
                }
                
            }

            return Result.Fail<FilteredOutputWorshipExtended>($"Богослужение с заданным Id={query.Id} не было найдено.");
        }
    }
}
