using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.EFSQLite
{
    /// <summary>
    /// Класс добавляет выборку для классов IAggregateRoot 
    /// </summary>
    internal class ClassPropertiesIncluder<DomainType> where DomainType : class, IAggregateRoot
    {
        private DbSet<DomainType> _dbSet;
        public ClassPropertiesIncluder(DbSet<DomainType> dbSet)
        {
            _dbSet = dbSet ?? throw new ArgumentNullException("DBSet");
        }

        public IQueryable<DomainType> GetIncludes()
        {
            IQueryable<DomainType> request = _dbSet.AsNoTracking();

            Type type = typeof(DomainType);
            switch (true)
            {
                case bool _ when type == typeof(TypiconEntity):
                    request = GetTypiconEntityIncludes();
                    break;
                case bool _ when type == typeof(DayWorship):
                    request = GetDayWorshipIncludes();
                    break;
                case bool _ when type == typeof(MenologyDay):
                    request = GetMenologyDayIncludes();
                    break;
            }

            return request;
        }

        private IQueryable<MenologyDay> MenologyDayInc(IQueryable<MenologyDay> request)
        {
            return request
                .Include(c => c.Date)
                .Include(c => c.DateB)
                .Include(c => c.DayWorships);
        }

        private IQueryable<DomainType> GetMenologyDayIncludes()
        {
            return (_dbSet as DbSet<MenologyDay>)
                .Include(c => c.Date)
                .Include(c => c.DateB)
                .Include(c => c.DayWorships)
                    .ThenInclude(c => c.WorshipName)
                .Include(c => c.DayWorships)
                    .ThenInclude(c => c.WorshipShortName)
                as IQueryable<DomainType>;
        }

        private IQueryable<DayWorship> DayWorshipInc(IQueryable<DayWorship> request)
        {
            return request
                .Include(c => c.WorshipName)
                .Include(c => c.WorshipShortName)
                as IQueryable<DayWorship>;
        }

        private IQueryable<DomainType> GetDayWorshipIncludes()
        {
            return (_dbSet as DbSet<DayWorship>)
                .Include(c => c.WorshipName)
                .Include(c => c.WorshipShortName)
                as IQueryable<DomainType>;
        }

        private IQueryable<DomainType> GetTypiconEntityIncludes()
        {
            return (_dbSet as DbSet<TypiconEntity>)
                .Include(c => c.Template)
                .Include(c => c.Signs)
                    .ThenInclude(c => c.SignName)
                .Include(c => c.CommonRules)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.Date)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DateB)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                .Include(c => c.Settings)
                .Include(c => c.ModifiedYears)
                    .ThenInclude(k => k.ModifiedRules)
                        .ThenInclude(c => c.RuleEntity) as IQueryable<DomainType>;
        }
    }
}
