using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Psalter;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconEntity : EntityBase<int>, IAggregateRoot
    {        
        public TypiconEntity()
        {
            Signs = new List<Sign>();
            ModifiedYears = new List<ModifiedYear>();
            CommonRules = new List<CommonRule>();
            MenologyRules = new List<MenologyRule>();
            TriodionRules = new List<TriodionRule>();
            Kathismas = new List<Kathisma>();
        }

        #region Properties

        public string Name { get; set; }

        /// <summary>
        /// Ссылка на Устав-шаблон.
        /// </summary>
        public virtual TypiconEntity Template { get; set; }

        /// <summary>
        /// Возвращает true, если у Устава нет шаблона
        /// </summary>
        public bool IsTemplate
        {
            get
            {
                return Template == null;
            }
        }

        /// <summary>
        /// Список знаков служб
        /// </summary>
        public virtual List<Sign> Signs { get; set; }

        public virtual List<ModifiedYear> ModifiedYears { get; set; }

        public virtual List<CommonRule> CommonRules { get; set; }
        public virtual List<MenologyRule> MenologyRules { get; set; }
        public virtual List<TriodionRule> TriodionRules { get; set; }

        public virtual List<Kathisma> Kathismas { get; set; }

        public virtual string DefaultLanguage { get; set; }

        #endregion

        protected override void Validate()
        {
            //TODO: Добавить валидацию TypiconEntity
            // GetAll().OfType - MenologyRules 

            // GetAll().OfType - TriodionRules 
            throw new NotImplementedException();
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

        public CommonRule GetCommonRule(Func<CommonRule, bool> predicate)
        {
            return CommonRules.FirstOrDefault(predicate);
        }

        #endregion
    }
}
