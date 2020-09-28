using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    /// <summary>
    /// Декоратор для <see cref="IRuleHandlerSettingsFactory"/>. Используется в <see cref="MajorDataCalculator"/>.
    /// Позволяет вычислить настройки только если служба удовлетворяет условиям графика богослужений
    /// 
    /// </summary>
    public class ScheduleSettingsDependingFactory : IRuleHandlerSettingsFactory
    {
        private readonly IRuleHandlerSettingsFactory _decoratee;
        private readonly TypiconDBContext _dbContext;

        public ScheduleSettingsDependingFactory(
            IRuleHandlerSettingsFactory decoratee
            , TypiconDBContext dbContext)
        {
            _decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Result<RuleHandlerSettings> CreateRecursive(CreateRuleSettingsRequest request)
        {
            //обрабатываем только последовательности богослужений, не "переходящие" последовательности
            if (request.RuleMode == RuleMode.ModRule)
            {
                return Decoratee(request);
            }

            var settings = _dbContext.Set<ScheduleSettings>()
                .FirstOrDefault(c => c.TypiconVersionId == request.TypiconVersionId);

            if (settings != null)
            {
                bool success = FromDaysOfWeek(settings, request)
                            || FromSigns(settings, request)
                            || FromMenologies(settings, request)
                            || FromTriodions(settings, request)
                            || FromIncluded(settings, request);

                //exclude
                success = success && FromExcluded(settings, request);

                if (success)
                {
                    return Decoratee(request);
                }
            }

            return Result.Fail<RuleHandlerSettings>("");
        }

        /// <summary>
        /// Из исключенных дат
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool FromExcluded(ScheduleSettings settings, CreateRuleSettingsRequest request)
        {
            return !settings.ExcludedDates.Contains(request.Date.Date);
        }

        /// <summary>
        /// Из включенных дат
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool FromIncluded(ScheduleSettings settings, CreateRuleSettingsRequest request)
        {
            return settings.IncludedDates.Any(c => c.Date == request.Date.Date);
        }

        /// <summary>
        /// Из правил триоди
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool FromTriodions(ScheduleSettings settings, CreateRuleSettingsRequest request)
        {
            return (request.Rule is TriodionRule triodion)
                && (settings.TriodionRules.Any(c => c.Rule == triodion));
        }

        /// <summary>
        /// Из правил минеи
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool FromMenologies(ScheduleSettings settings, CreateRuleSettingsRequest request)
        {
            return (request.Rule is MenologyRule menology)
                && (settings.MenologyRules.Any(c => c.Rule == menology));
        }

        /// <summary>
        /// Из Знаков службы
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool FromSigns(ScheduleSettings settings, CreateRuleSettingsRequest request)
        {
            return settings.Signs
                .Any(c => c.Rule == request.Rule.Template); 
        }

        private Result<RuleHandlerSettings> Decoratee(CreateRuleSettingsRequest request) => _decoratee.CreateRecursive(request);

        /// <summary>
        /// Из выбранных дней недели
        /// </summary>
        /// <param name="s"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool FromDaysOfWeek(ScheduleSettings s, CreateRuleSettingsRequest r)
        {
            return (s.IsMonday && r.Date.DayOfWeek == DayOfWeek.Monday)
                || (s.IsTuesday && r.Date.DayOfWeek == DayOfWeek.Tuesday)
                || (s.IsWednesday && r.Date.DayOfWeek == DayOfWeek.Wednesday)
                || (s.IsThursday && r.Date.DayOfWeek == DayOfWeek.Thursday)
                || (s.IsFriday && r.Date.DayOfWeek == DayOfWeek.Friday)
                || (s.IsSaturday && r.Date.DayOfWeek == DayOfWeek.Saturday)
                || (s.IsSunday && r.Date.DayOfWeek == DayOfWeek.Sunday);
        }

        public Result<RuleHandlerSettings> CreateExplicit(CreateExplicitRuleSettingsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
