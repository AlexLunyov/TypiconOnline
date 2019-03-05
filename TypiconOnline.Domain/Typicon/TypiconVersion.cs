using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Версия Устава
    /// </summary>
    public class TypiconVersion : EntityBase<int>
    {
        #region Properties

        public virtual ItemText Name { get; set; }
        /// <summary>
        /// Языка по умолчанию
        /// </summary>
        public virtual string DefaultLanguage { get; set; }

        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual IEnumerable<Sign> Signs { get; set; }
        /// <summary>
        /// Года с вычисленными переходящими праздниками
        /// </summary>
        public virtual IEnumerable<ModifiedYear> ModifiedYears { get; set; }

        public virtual IEnumerable<CommonRule> CommonRules { get; set; }
        public virtual IEnumerable<MenologyRule> MenologyRules { get; set; }
        public virtual IEnumerable<TriodionRule> TriodionRules { get; set; }

        public virtual IEnumerable<Kathisma> Kathismas { get; set; }

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

        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}