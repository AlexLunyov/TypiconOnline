using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Версия Устава
    /// </summary>
    public class TypiconVersion : ValueObjectBase<IRuleSerializerRoot>, IHasId<int>
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
        }

        #region Properties
        public int Id { get; set; }
        public int TypiconId { get; set; }
        /// <summary>
        /// Ссылка на сущность Устава
        /// </summary>
        public virtual TypiconEntity Typicon { get; set; }

        public virtual ItemText Name { get; set; }
        /// <summary>
        /// Языка по умолчанию
        /// </summary>
        public virtual string DefaultLanguage { get; set; }

        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual List<Sign> Signs { get; set; }
        /// <summary>
        /// Года с вычисленными переходящими праздниками
        /// </summary>
        public virtual List<ModifiedYear> ModifiedYears { get; set; }

        public virtual List<CommonRule> CommonRules { get; set; }
        public virtual List<MenologyRule> MenologyRules { get; set; }
        public virtual List<TriodionRule> TriodionRules { get; set; }

        public virtual List<Kathisma> Kathismas { get; set; }

        public virtual List<ExplicitAddRule> ExplicitAddRules { get; set; }

        /// <summary>
        /// Признак того, что любое из дочерних свойств было изменено.
        /// Необходимо для индикации необходимости провести перевычисление ModifiedYears
        /// 
        /// ???
        /// </summary>
        public virtual bool IsModified { get; set; }

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

        #endregion

        protected override void Validate(IRuleSerializerRoot ruleSerializer)
        {
            throw new System.NotImplementedException();
        }

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
}
