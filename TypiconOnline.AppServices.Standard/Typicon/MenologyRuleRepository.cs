using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Standard.Typicon
{
    public class MenologyRuleRepository
    {
        TypiconDBContext dbContext;
        IUnitOfWork unitOfWork;

        public MenologyRuleRepository(TypiconDBContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext in MenologyRuleRepository");
        }

        public MenologyRuleRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork in MenologyRuleRepository");
        }

        public MenologyRule Get(int typiconId, DateTime date)
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

            return dbContext.Set<MenologyRule>()
                .Include(c => c.Date)
                .Include(c => c.DateB)
                .Include(c => c.Template)
                    .ThenInclude(c => c.Template)
                .Include(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                .Include(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                .FirstOrDefault(expression);
        }

        public MenologyRule GetFromUOW(int typiconId, DateTime date)
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

            var options = new IncludeOptions()
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

            return unitOfWork.Repository<MenologyRule>().Get(expression, options);
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
