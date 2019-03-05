using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.AppServices.Implementations
{
    public class TypiconFromEntityFacade : ITypiconFacade
    {
        private readonly TypiconDBContext _context;

        public TypiconFromEntityFacade([NotNull] TypiconDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof (context));
        }

        public IEnumerable<MenologyRule> GetAllMenologyRules(int typiconId)
        {
            return GetTypiconEntity(typiconId)?.MenologyRules;
        }

        public IEnumerable<TriodionRule> GetAllTriodionRules(int typiconId)
        {
            return GetTypiconEntity(typiconId)?.TriodionRules;
        }

        public DateTime GetCurrentEaster(int year)
        {
            var easterItem = _context.Set<EasterItem>().FirstOrDefault(c => c.Date.Year == year);

            return (easterItem != null) ? easterItem.Date : DateTime.MinValue;
        }

        public MenologyRule GetMenologyRule(int typiconId, DateTime date)
        {
            return GetTypiconEntity(typiconId)?.GetMenologyRule(date);
        }

        public ModifiedRule GetModifiedRuleHighestPriority(int typiconId, DateTime date)
        {
            var found = _context.Set<ModifiedRule>().Where(c => c.Parent.TypiconEntityId == typiconId
                && c.Date.Date == date.Date).ToList();
            return (found != null) ? found.Min() : default(ModifiedRule);
        }

        public TriodionRule GetTriodionRule(int typiconId, DateTime date)
        {
            var currentEaster = GetCurrentEaster(date.Year);

            int daysFromEaster = date.Date.Subtract(currentEaster.Date.Date).Days;

            return GetTypiconEntity(typiconId)?.GetTriodionRule(daysFromEaster);
        }


        TypiconEntity _typiconEntity;

        private TypiconEntity GetTypiconEntity(int typiconId)
        {
            if (_typiconEntity == null || _typiconEntity.Id != typiconId)
            {
                //подгружаем знаки служб
                _context.Set<Sign>()
                    .Where(c => c.TypiconEntityId == typiconId)
                    .Include(c => c.SignName)
                    .Load();

                var request = GetTypiconEntityIncludes(_context.Set<TypiconEntity>());

                _typiconEntity = request.Where(c => c.Id == typiconId).FirstOrDefault();
            }
            return _typiconEntity;
        }

        private IQueryable<TypiconEntity> GetTypiconEntityIncludes(DbSet<TypiconEntity> dbSet)
        {
            return dbSet
                .Include(c => c.Template)
                //.Include(c => c.Signs)
                //    .ThenInclude(c => c.Template)
                .Include(c => c.CommonRules)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.Date)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DateB)
                .Include(c => c.MenologyRules)
                    //.ThenInclude(c => c.Template)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                                .ThenInclude(k => k.Items)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                                .ThenInclude(k => k.Items)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.Parent)
                .Include(c => c.TriodionRules)
                    //.ThenInclude(c => c.Template)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                                .ThenInclude(k => k.Items)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                                .ThenInclude(k => k.Items)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.Parent)
                .Include(c => c.ModifiedYears)
                    .ThenInclude(k => k.ModifiedRules)
                        .ThenInclude(c => c.DayRule)
                 .Include(c => c.ModifiedYears)
                    .ThenInclude(k => k.ModifiedRules)
                        .ThenInclude(c => c.Filter)
                //.Include(c => c.ModifiedYears)
                //    .ThenInclude(k => k.ModifiedRules)
                //        .ThenInclude(c => c.ShortName)
                .Include(c => c.Kathismas)
                    .ThenInclude(c => c.SlavaElements)
                        .ThenInclude(c => c.PsalmLinks)
                            .ThenInclude(c => c.Psalm);
        }
    }
}
