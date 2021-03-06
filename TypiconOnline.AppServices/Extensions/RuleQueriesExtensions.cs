﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Extensions
{
    public static class RuleQueriesExtensions
    {
        public static MenologyRule GetMenologyRule(this IEnumerable<MenologyRule> menologyRules, DateTime date)
        {
            return menologyRules.FirstOrDefault(c => c.GetCurrentDate(date.Year).Date == date.Date);
        }

        public static IEnumerable<MenologyRule> GetAllMovableRules(this IEnumerable<MenologyRule> menologyRules)
        {
            return menologyRules.Where(c => c.Date.IsEmpty && c.LeapDate.IsEmpty);
        }

        public static IEnumerable<MenologyRule> GetAllMenologyRules(this TypiconDBContext dbContext, int typiconVersionId)
        {
            return dbContext.Set<MenologyRule>().Where(c => c.TypiconVersionId == typiconVersionId).ToList();
        }

        #region не используемое

        public static IEnumerable<MenologyRule> GetAllMovableMenologyRules(this TypiconDBContext dbContext, int typiconVersionId)
        {
            return dbContext.Set<MenologyRule>()
                .Where(c => c.TypiconVersionId == typiconVersionId
                            && c.ModRuleDefinition != string.Empty)
                .ToList()
                .Where(c => c.Date.IsEmpty && c.LeapDate.IsEmpty);
        }

        /// <summary>
        /// Возвращает все непереходящие праздники
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="typiconVersionId"></param>
        /// <returns></returns>
        public static IEnumerable<MenologyRule> GetAllModMenologyRules(this TypiconDBContext dbContext, int typiconVersionId)
        {
            return dbContext.Set<MenologyRule>()
                .Where(c => c.TypiconVersionId == typiconVersionId
                            && c.ModRuleDefinition != string.Empty)
                .ToList()
                .Where(c => !c.LeapDate.IsEmpty);
        }

        #endregion

        public static MenologyRule GetMenologyRule(this TypiconDBContext dbContext, int typiconVersionId, DateTime date)
        {
            return (DateTime.IsLeapYear(date.Year))
                ? dbContext.Set<MenologyRule>().FirstOrDefault(c => c.TypiconVersionId == typiconVersionId 
                                                                    && c.LeapDate.Day == date.Day 
                                                                    && c.LeapDate.Month == date.Month)
                : dbContext.Set<MenologyRule>().FirstOrDefault(c => c.TypiconVersionId == typiconVersionId
                                                                    && c.Date.Day == date.Day 
                                                                    && c.Date.Month == date.Month);
        }

        public static IEnumerable<TriodionRule> GetAllTriodionRules(this TypiconDBContext dbContext, int typiconVersionId)
        {
            return dbContext.Set<TriodionRule>().Where(c => c.TypiconVersionId == typiconVersionId).ToList();
        }

        public static TriodionRule GetTriodionRule(this TypiconDBContext dbContext, int typiconVersionId, DateTime date)
        {
            var currentEaster = dbContext.GetCurrentEaster(date.Year);

            int daysFromEaster = date.Date.Subtract(currentEaster.Date).Days;

            return dbContext.Set<TriodionRule>()
                .FirstOrDefault(c => c.TypiconVersionId == typiconVersionId && c.DaysFromEaster == daysFromEaster);
        }

        public static T GetRule<T>(this TypiconDBContext dbContext, int id) where T : RuleEntity, new()
        {
            return dbContext.Set<T>().FirstOrDefault(c => c.Id == id);
        }

        public static PrintDayTemplate GetPrintDayTemplate(this TypiconDBContext dbContext, int typiconVersionId, int number)
        {
            return dbContext.Set<PrintDayTemplate>().FirstOrDefault(c => c.TypiconVersionId == typiconVersionId
                                                                    && c.Number == number);
        }
    }
}
