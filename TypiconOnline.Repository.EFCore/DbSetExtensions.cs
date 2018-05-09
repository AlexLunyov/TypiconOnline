using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.EFCore
{
    /// <summary>
    /// Класс добавляет выборку для классов IAggregateRoot 
    /// </summary>
    internal static class DbSetExtensions
    {
        public static IQueryable<DomainType> GetIncludes<DomainType>(this DbSet<DomainType> dbSet, IncludeOptions options) where DomainType : class
        {
            //IQueryable<DomainType> request = dbSet;

            //Type type = typeof(DomainType);

            ////смотрим каждое свойство
            //foreach (var property in type.GetProperties())
            //{
            //    //добавляем одиночные свойства
            //    if (options == null || options.IncludeSingleEntities)
            //    {
            //        if (IsClass(property.PropertyType))
            //        {
            //            request = request.Include(property.Name);
            //        }
            //    }

            //    //добавляем коллекции
            //    if (options == null || options.IncludeCollections)
            //    {
            //        if (IsNonStringEnumerable(property.PropertyType))
            //        {
            //            //для каждого свойства вложенного класса
            //            //также просматриваем, класс ли это или коллекция и подгружаем
            //            //но только через метод ThenInclude
            //            request = request.Include(property.Name);

            //            request = AddThenIncludes
            //        }
            //    }
            //}

            IQueryable<DomainType> request = dbSet;

            if (options == null || options.LoadRelatedEntities)
            {
                request = dbSet.GetIncludes();
            }

            return request;
        }

        private static bool IsClass(Type propertyType)
        {
            return propertyType.IsClass;
        }

        /// <summary>
        /// Возвращает, является ли Тип коллекцией, за исключением строкового типа
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsNonStringEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
                return false;
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public static IQueryable<DomainType> GetIncludes<DomainType>(this DbSet<DomainType> dbSet) 
            where DomainType : class//, IAggregateRoot
        {
            //TODO: убираем AsNoTracking - необходимо протестировать ScheduleService 
            //- не будут ли портиться связи и ссылки на службы
            IQueryable<DomainType> request = dbSet;//.AsNoTracking();

            Type type = typeof(DomainType);
            switch (true)
            {
                case bool _ when type == typeof(TypiconEntity):
                    request = GetTypiconEntityIncludes(dbSet);
                    break;
                case bool _ when type == typeof(DayWorship):
                    request = GetDayWorshipIncludes(dbSet);
                    break;
                case bool _ when type == typeof(MenologyDay):
                    request = GetMenologyDayIncludes(dbSet);
                    break;
                case bool _ when type == typeof(DayRule):
                    request = GetDayRuleIncludes(dbSet);
                    break;
                default:
                    //nothing
                    break;
            }

            return request;
        }

        private static IQueryable<DomainType> GetDayRuleIncludes<DomainType>(DbSet<DomainType> dbSet) where DomainType : class
        {
            return (dbSet as DbSet<DayRule>)
                .Include(c => c.Template)
                .Include(c => c.DayWorships)
                    .ThenInclude(c => c.WorshipName)
                .Include(c => c.DayWorships)
                    .ThenInclude(c => c.WorshipShortName)
                as IQueryable<DomainType>;
        }

        private static IQueryable<MenologyDay> MenologyDayInc<DomainType>(IQueryable<MenologyDay> request) where DomainType : class
        {
            return request
                .Include(c => c.Date)
                .Include(c => c.DateB)
                .Include(c => c.DayWorships);
        }

        private static IQueryable<DomainType> GetMenologyDayIncludes<DomainType>(DbSet<DomainType> dbSet) where DomainType : class
        {
            return (dbSet as DbSet<MenologyDay>)
                .Include(c => c.Date)
                .Include(c => c.DateB)
                .Include(c => c.DayWorships)
                    .ThenInclude(c => c.WorshipName)
                .Include(c => c.DayWorships)
                    .ThenInclude(c => c.WorshipShortName)
                as IQueryable<DomainType>;
        }

        private static IQueryable<DayWorship> DayWorshipInc<DomainType>(IQueryable<DayWorship> request) where DomainType : class
        {
            return request
                .Include(c => c.WorshipName)
                .Include(c => c.WorshipShortName)
                as IQueryable<DayWorship>;
        }

        private static IQueryable<DomainType> GetDayWorshipIncludes<DomainType>(DbSet<DomainType> dbSet) where DomainType : class
        {
            return (dbSet as DbSet<DayWorship>)
                .Include(c => c.WorshipName)
                .Include(c => c.WorshipShortName)
                as IQueryable<DomainType>;
        }

        private static IQueryable<DomainType> GetTypiconEntityIncludes<DomainType>(DbSet<DomainType> dbSet) where DomainType : class
        {
            return (dbSet as DbSet<TypiconEntity>)
                .Include(c => c.Template)
                .Include(c => c.Signs)
                    .ThenInclude(c => c.SignName)
                .Include(c => c.Signs)
                    .ThenInclude(c => c.Template)
                .Include(c => c.CommonRules)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.Date)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DateB)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.Template)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                .Include(c => c.MenologyRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.Parent)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.Template)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipName)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.WorshipShortName)
                .Include(c => c.TriodionRules)
                    .ThenInclude(c => c.DayRuleWorships)
                        .ThenInclude(k => k.DayWorship)
                            .ThenInclude(k => k.Parent)
                .Include(c => c.Settings)
                .Include(c => c.ModifiedYears)
                    .ThenInclude(k => k.ModifiedRules)
                        .ThenInclude(c => c.RuleEntity)
                 .Include(c => c.ModifiedYears)
                    .ThenInclude(k => k.ModifiedRules)
                        .ThenInclude(c => c.Filter)
                //.Include(c => c.ModifiedYears)
                //    .ThenInclude(k => k.ModifiedRules)
                //        .ThenInclude(c => c.ShortName)
                .Include(c => c.Kathismas)
                    .ThenInclude(c => c.SlavaElements)
                        .ThenInclude(c => c.PsalmLinks)
                            .ThenInclude(c => c.Psalm) as IQueryable<DomainType>;
        }
    }
}
