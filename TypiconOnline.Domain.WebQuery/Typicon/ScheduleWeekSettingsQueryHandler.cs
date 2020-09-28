using JetBrains.Annotations;
using System.Linq;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.AppServices.OutputFiltering;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает фильтрованную Выходную форму для недели
    /// </summary>
    public class ScheduleWeekSettingsQueryHandler : DbContextQueryBase, IQueryHandler<ScheduleWeekSettingsQuery, Result<ScheduleSettingsWeekDaysModel>>
    {
        public ScheduleWeekSettingsQueryHandler(TypiconDBContext dbContext)
            : base(dbContext) { }

        public Result<ScheduleSettingsWeekDaysModel> Handle([NotNull] ScheduleWeekSettingsQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                .Where(TypiconVersion.IsDraft)
                .FirstOrDefault(c => c.TypiconId == query.TypiconId);

            if (draft != null)
            {
                var model = new ScheduleSettingsWeekDaysModel()
                {
                    TypiconId = query.TypiconId
                };

                if (draft.ScheduleSettings is ScheduleSettings settings)
                {
                    model.IsMonday = settings.IsMonday;
                    model.IsTuesday = settings.IsTuesday;
                    model.IsWednesday = settings.IsWednesday;
                    model.IsThursday = settings.IsThursday;
                    model.IsFriday = settings.IsFriday;
                    model.IsSaturday = settings.IsSaturday;
                    model.IsSunday = settings.IsSunday;

                    //model.Signs.AddRange(settings.Signs.Select(c => new SignScheduleModel()
                    //{
                    //    RuleId = c.SignId,
                    //    PrintTemplateId = c.Sign.PrintTemplateId,
                    //    Name = c.Sign.SignName.FirstOrDefault(CommonConstants.DefaultLanguage).Text
                    //}));

                    //model.MenologyRules.AddRange(settings.MenologyRules.Select(c => new MenologyRuleScheduleModel()
                    //{
                    //    RuleId = c.RuleId,
                    //    Date = c.Rule.Date.Expression,
                    //    LeapDate = c.Rule.LeapDate.Expression,
                    //    Name = c.Rule.GetNameByLanguage(CommonConstants.DefaultLanguage),
                    //    SignName = c.Rule.Template.SignName.FirstOrDefault(CommonConstants.DefaultLanguage).Text
                    //}));

                    //model.TriodionRules.AddRange(settings.TriodionRules.Select(c => new TriodionRuleScheduleModel()
                    //{
                    //    RuleId = c.RuleId,
                    //    DaysFromEaster = c.Rule.DaysFromEaster,
                    //    Name = c.Rule.GetNameByLanguage(CommonConstants.DefaultLanguage),
                    //    SignName = c.Rule.Template.SignName.FirstOrDefault(CommonConstants.DefaultLanguage).Text
                    //}));

                    model.IncludedDates.AddRange(settings.IncludedDates);
                    model.ExcludedDates.AddRange(settings.ExcludedDates);
                }

                return Result.Ok(model);
            }
            else
            {
                return Result.Fail<ScheduleSettingsWeekDaysModel>(404, $"Устав с Id={query.TypiconId} не найден.");
            }
        }
    }
}
