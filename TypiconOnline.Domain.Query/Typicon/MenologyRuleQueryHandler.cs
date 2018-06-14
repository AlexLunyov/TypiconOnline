using JetBrains.Annotations;
using System;
using System.Linq.Expressions;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class MenologyRuleQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<MenologyRuleQuery, MenologyRule>
    {
        public MenologyRuleQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        private readonly IncludeOptions Includes = new IncludeOptions()
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

        public MenologyRule Handle([NotNull] MenologyRuleQuery query)
        {
            Expression<Func<MenologyRule, bool>> expression;

            string dateString = GetItemDateString(query.Date);

            if (DateTime.IsLeapYear(query.Date.Year))
            {
                expression = c => c.OwnerId == query.TypiconId && c.DateB.Expression == dateString;
            }
            else
            {
                expression = c => c.OwnerId == query.TypiconId && c.Date.Expression == dateString;
            }

            return UnitOfWork.Repository<MenologyRule>().Get(expression, Includes);
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
