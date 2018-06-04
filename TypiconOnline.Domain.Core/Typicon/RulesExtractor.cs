using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Core.Typicon
{
    /// <summary>
    /// Извлекает связанные Правила для конкретного Устава
    /// </summary>
    public class RulesExtractor : IRulesExtractor
    {
        IUnitOfWork unitOfWork;

        public RulesExtractor(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in RulesExtractor");
        }

        #region Includes

        private readonly IncludeOptions MenologyRuleIncludes = new IncludeOptions()
        {
            Includes = new string[]
            {
                    "Date",
                    "DateB",
                    "Template.Template.Template",
                    "DayRuleWorships.DayWorship.WorshipName",
                    "DayRuleWorships.DayWorship.WorshipShortName",
            }
        };

        private readonly IncludeOptions TriodionRuleIncludes = new IncludeOptions()
        {
            Includes = new string[]
            {
                    "Template.Template.Template",
                    "DayRuleWorships.DayWorship.WorshipName",
                    "DayRuleWorships.DayWorship.WorshipShortName",
            }
        };

        #endregion

        public IEnumerable<MenologyRule> GetAllMenologyRules(int typiconId)
        {
            return unitOfWork.Repository<MenologyRule>().GetAll(c => c.OwnerId == typiconId, MenologyRuleIncludes).ToList();
        }

        public IEnumerable<TriodionRule> GetAllTriodionRules(int typiconId)
        {
            return unitOfWork.Repository<TriodionRule>().GetAll(c => c.OwnerId == typiconId, TriodionRuleIncludes).ToList();
        }

        public CommonRule GetCommonRule(int typiconId, string name)
        {
            return unitOfWork.Repository<CommonRule>().Get(c => c.OwnerId == typiconId && c.Name == name);
        }

        public MenologyRule GetMenologyRule(int typiconId, DateTime date)
        {
            Expression<Func<MenologyRule, bool>> expression;

            string dateString = GetItemDateString(date);

            if (DateTime.IsLeapYear(date.Year))
            {
                expression = c => c.OwnerId == typiconId && c.DateB.Expression == dateString;
            }
            else
            {
                expression = c => c.OwnerId == typiconId && c.Date.Expression == dateString;
            }

            return unitOfWork.Repository<MenologyRule>().Get(expression, MenologyRuleIncludes);
        }

        public TriodionRule GetTriodionRule(int typiconId, int daysFromEaster)
        {
            return unitOfWork.Repository<TriodionRule>().Get(c => c.OwnerId == typiconId && c.DaysFromEaster == daysFromEaster, TriodionRuleIncludes);
        }

        /// <summary>
        /// Возвращает конкретную дату в году, когда совершается данная служба
        /// </summary>
        /// <param name="year">Конкретный год</param>
        /// <returns>Если поля Date или DateB пустые, вовращает пустое (минимальное) значение</returns>
        private string GetItemDateString(DateTime date)
        {
            return new ItemDate(date.Month, date.Day).ToString();
        }

    }
}
