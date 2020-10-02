using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Версия Устава
    /// </summary>
    public class TypiconVersion : /*ValueObjectBase<IRuleSerializerRoot>,*/ IHasId<int>
    {
        public TypiconVersion()
        {
            CDate = DateTime.Now;
            Signs = new List<Sign>();
            ModifiedYears = new List<ModifiedYear>();
            CommonRules = new List<CommonRule>();
            MenologyRules = new List<MenologyRule>();
            TriodionRules = new List<TriodionRule>();
            Kathismas = new List<Kathisma>();
            ExplicitAddRules = new List<ExplicitAddRule>();

            TypiconVariables = new List<TypiconVariable>();

            PrintDayTemplates = new List<PrintDayTemplate>();

            ValidationStatus = ValidationStatus.NotValidated;
            Errors = new List<TypiconVersionError>();
        }

        #region Properties
        public int Id { get; set; }
        public int TypiconId { get; set; }
        /// <summary>
        /// Ссылка на сущность Устава
        /// </summary>
        public virtual TypiconEntity Typicon { get; set; }
        /// <summary>
        /// Id предыдущей версии TypiconVersion
        /// </summary>
        public int? PrevVersionId { get; set; }

        /// <summary>
        /// Ссылка на предыдущую версию Устава
        /// </summary>
        public virtual TypiconVersion PrevVersion { get; set; }

        public virtual ItemText Name { get; set; }
        public virtual ItemText Description { get; set; }

        /// <summary>
        /// Настройки дней, когда совершаются богослужения
        /// </summary>
        public virtual ScheduleSettings ScheduleSettings { get; set; }

        /// <summary>
        /// Номер Версии Устава. При создании новых версий, номер увеличивается на единицу
        /// </summary>
        public int VersionNumber { get; set; }

        /// <summary>
        /// Года с вычисленными переходящими праздниками
        /// </summary>
        public virtual List<ModifiedYear> ModifiedYears { get; set; }
        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual List<Sign> Signs { get; set; }
        /// <summary>
        /// Общие Правила
        /// </summary>
        public virtual List<CommonRule> CommonRules { get; set; }
        /// <summary>
        /// Правила Минеи
        /// </summary>
        public virtual List<MenologyRule> MenologyRules { get; set; }
        /// <summary>
        /// Правила Триоди
        /// </summary>
        public virtual List<TriodionRule> TriodionRules { get; set; }
        /// <summary>
        /// Кафизмы
        /// </summary>
        public virtual List<Kathisma> Kathismas { get; set; }
        /// <summary>
        /// Явно определяемые дополнительные Правила
        /// </summary>
        public virtual List<ExplicitAddRule> ExplicitAddRules { get; set; }
        /// <summary>
        /// Переменные Устава
        /// </summary>
        public virtual List<TypiconVariable> TypiconVariables { get; set; }
        /// <summary>
        /// Печатный шаблон седмицы. Используется в формировани печатной версии Расписания на неделю
        /// </summary>
        public virtual PrintWeekTemplate PrintWeekTemplate { get; set; }

        /// <summary>
        /// Печатный шаблон по умолчанию.
        /// Используется, когда богослужения совершаются не ежедневно.
        /// Используется для отображения вечерних богослужений тех дней, в которые утром не совершалось богослужение.
        /// </summary>
        public virtual PrintDayTemplate PrintDayDefaultTemplate { get; set; }
        /// <summary>
        /// Печатные шаблоны дней. Используются в формировани печатной версии Расписания на неделю
        /// </summary>
        public virtual List<PrintDayTemplate> PrintDayTemplates { get; set; }

        /// <summary>
        /// Признак того, что любое из дочерних свойств было изменено.
        /// Необходимо для индикации необходимости провести перевычисление ModifiedYears
        /// 
        /// ???
        /// </summary>
        public virtual bool IsModified { get; set; }

        /// <summary>
        /// Признак, является ли Версия Шаблоном (может иметь Переменные и быть опубликована)
        /// </summary>
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Дата создания черновика
        /// </summary>
        public DateTime CDate { get; set; }
        /// <summary>
        /// Дата начала публикации. Если заполнено, то значит версия Устава была опубликована.
        /// </summary>
        public DateTime? BDate { get; set; }
        /// <summary>
        /// Дата окончания публикации. Выставляется, когда версия отправляется в архив при сохранении новой актуальной версии Устава.
        /// </summary>
        public DateTime? EDate { get; set; }

        public ValidationStatus ValidationStatus { get; set; }
        
        //TODO: Убрать - не используется
        protected List<TypiconVersionError> Errors { get; set; }
        /// <summary>
        /// Признак того, был ли объект валидирован.
        /// Необходимо обнулять его при каждом изменении внутренних свойств
        /// </summary>
        private bool IsValidated { get; set; } = false;

        #endregion

        #region Lambdas

        public static Expression<Func<TypiconVersion, bool>> IsPublished = c => c.BDate != null && c.EDate == null;
        public static Expression<Func<TypiconVersion, bool>> IsDraft = c => c.BDate == null && c.EDate == null;
        
        //public bool IsPublished => BDate != null && EDate == null;
        //public bool IsDraft => BDate == null && EDate == null;
        #endregion

        #region Validation

        public IReadOnlyCollection<TypiconVersionError> GetBrokenConstraints(IRuleSerializerRoot ruleSerializer)
        {
            if (!IsValidated)
            {
                Errors.Clear();
                Validate(ruleSerializer);
                IsValidated = true;
            }

            return Errors;
        }

        protected void Validate(IRuleSerializerRoot ruleSerializer)
        {
            if (Typicon == null || TypiconId == 0)
            {
                AddError("Версия Устава должна иметь ссылку на Устав");
            }

            //Signs
            ValidateChildCollection(Signs.Cast<RuleEntity>(), ruleSerializer, ErrorConstants.Sign, ErrorConstants.Signs, "Должен быть определен хотя бы один Знак службы");
            //CommonRules
            ValidateChildCollection(CommonRules.Cast<RuleEntity>(), ruleSerializer, ErrorConstants.CommonRule);
            //MenologyRules
            ValidateMenologyRules(ruleSerializer);
            //TriodionRules
            ValidateChildCollection(TriodionRules.Cast<RuleEntity>(), ruleSerializer, ErrorConstants.TriodionRule);
            //ExplicitAddRules
            ValidateChildCollection(ExplicitAddRules.Cast<RuleEntity>(), ruleSerializer, ErrorConstants.ExplicitAddRule);
            //Kathismas
            ValidateKathismas(ruleSerializer.TypiconSerializer);

            if (ScheduleSettings == null)
            {
                AddError("Версия Устава должна иметь Настройки дней богослужений");
            }
        }

        private void ValidateMenologyRules(IRuleSerializerRoot ruleSerializer)
        {
            //проверяем наличие Правил для каждого дня високосного года
            EachDayPerYear.Perform(2016, date =>
            {
                if (GetMenologyRule(date) == null)
                {
                    AddError($"Отсутствует определение Правила Минеи для даты --{date.Month}-{date.Day}", ErrorConstants.MenologyRules);
                }
            });

            ValidateChildCollection(MenologyRules.Cast<RuleEntity>(), ruleSerializer, ErrorConstants.MenologyRule);
        }

        private void ValidateKathismas(ITypiconSerializer serializer)
        {
            if (Kathismas.Count != 20)
            {
                AddError("Должно быть определено 20 кафизм", ErrorConstants.Kathismas);
            }

            foreach (var entity in Kathismas)
            {
                var err = entity.GetBrokenConstraints(serializer);
                if (err.Count > 0)
                {
                    AddError(err.GetSummary(), ErrorConstants.Kathisma, entity.Id);
                }
            }
        }

        private void ValidateChildCollection(IEnumerable<RuleEntity> entities, IRuleSerializerRoot ruleSerializer, string entityName, string entitiesName = null, string requiredErrMessage = null)
        {
            if (!string.IsNullOrEmpty(requiredErrMessage))
            {
                //проверяем на обязательность хотя бы одного дочернего элемента
                if (entities.Count() == 0)
                {
                    AddError(requiredErrMessage, entitiesName);
                }
            }

            foreach (var entity in entities)
            {
                var err = entity.GetBrokenConstraints(ruleSerializer);
                if (err.Count > 0)
                {
                    AddError(err.GetSummary(), entityName, entity.Id);
                }
            }
        }

        private void AddError(string principleDescription, string entityName = null, int? entityId = null)
        {
            Errors.Add(new TypiconVersionError(Id, principleDescription, entityName, entityId));
        }

        #endregion

        #region GetRule methods

        public MenologyRule GetMenologyRule(DateTime date)
        {
            return MenologyRules.FirstOrDefault(c => c.GetCurrentDate(date.Year).Date == date.Date);
        }

        public TriodionRule GetTriodionRule(int daysFromEaster)
        {
            return TriodionRules.FirstOrDefault(c => c.DaysFromEaster == daysFromEaster);
        }
        #endregion
    }

    public enum ValidationStatus
    {
        NotValidated = 0,
        InProcess = 1,
        Invalid = 2,
        Valid = 3
    }
}
