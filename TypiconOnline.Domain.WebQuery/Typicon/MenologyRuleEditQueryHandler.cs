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
    public class MenologyRuleEditQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<MenologyRuleEditQuery, Result<MenologyRuleEditModel>>
    {
        private const int YEAR = 2017;
        private const int LEAP_YEAR = 2016;
        public MenologyRuleEditQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<MenologyRuleEditModel> Handle([NotNull] MenologyRuleEditQuery query)
        {
            var found = DbContext.Set<MenologyRule>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                found.DayRuleWorships.Sort();
                var model = new MenologyRuleEditModel()
                {
                    Id = found.Id,
                    IsAddition = found.IsAddition,
                    TemplateId = found.TemplateId,
                    ModRuleDefinition = found.ModRuleDefinition,
                    RuleDefinition = found.RuleDefinition,
                    DayWorships = found.DayRuleWorships
                        .Select(c => new MenologyDayWorshipModel()
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

                if (found.Date?.IsEmpty == false)
                {
                    model.Date = new DateTime(YEAR, found.Date.Month, found.Date.Day);
                }

                if (found.LeapDate?.IsEmpty == false)
                {
                    model.LeapDate = new DateTime(LEAP_YEAR, found.LeapDate.Month, found.LeapDate.Day);
                }

                return Result.Ok(model);
            }
            else
            {
                return Result.Fail<MenologyRuleEditModel>($"Правило с Id={query.Id} не найден.");
            }
        }
    }
}
