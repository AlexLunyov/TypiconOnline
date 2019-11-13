using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class TriodionRuleEditQueryHandler : QueryStrategyHandlerBase, IQueryHandler<TriodionRuleEditQuery, Result<TriodionRuleCreateEditModel>>
    {
        public TriodionRuleEditQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<TriodionRuleCreateEditModel> Handle([NotNull] TriodionRuleEditQuery query)
        {
            var found = DbContext.Set<TriodionRule>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                found.DayRuleWorships.Sort();
                var model = new TriodionRuleCreateEditModel()
                {
                    Id = found.Id,
                    TemplateId = found.TemplateId,
                    DaysFromEaster = found.DaysFromEaster,
                    IsTransparent = found.IsTransparent,
                    ModRuleDefinition = found.ModRuleDefinition,
                    RuleDefinition = found.RuleDefinition,
                    DayWorships = found.DayRuleWorships
                        .Select(c => new TriodionDayWorshipModel()
                        {
                            WorshipId = c.DayWorship.Id,
                            Order = c.Order,
                            Name = c.DayWorship.WorshipName?.ToString(query.Language),
                            ShortName = c.DayWorship.WorshipShortName?.ToString(query.Language),
                            IsCelebrating = c.DayWorship.IsCelebrating,
                            UseFullName = c.DayWorship.UseFullName
                        })
                        .ToList()
                };

                return Result.Ok(model);
            }
            else
            {
                return Result.Fail<TriodionRuleCreateEditModel>($"Правило с Id={query.Id} не найден.");
            }
        }
    }
}
