﻿using System;
using System.Collections.Generic;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFSQLite.DataBase;
using System.Linq.Expressions;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain;
using Microsoft.EntityFrameworkCore;
//using System.Data;

namespace TypiconOnline.Repository.EFSQLite
{
    public class TypiconEntitiesRepository : IRepository<TypiconEntity>
    {
        private SQLiteDBContext _typiconDBContext = null;
        DbSet<TypiconEntity> _objectSet;

        public TypiconEntitiesRepository(SQLiteDBContext typiconDBContext)
        {
            _typiconDBContext = typiconDBContext;
            _objectSet = _typiconDBContext.Set<TypiconEntity>();
        }

        public IEnumerable<TypiconEntity> GetAll(Expression<Func<TypiconEntity, bool>> predicate = null)
        {
            IQueryable<TypiconEntity> response = _objectSet
                    .Include(c => c.Template)
                    .Include(c => c.Signs)
                        .ThenInclude(c => c.SignName)
                    .Include(c => c.CommonRules)
                    .Include(c => c.MenologyRules)
                        .ThenInclude(c => c.Date)
                    .Include(c => c.MenologyRules)
                        .ThenInclude(c => c.DateB)
                    .Include(c => c.TriodionRules);

            if (predicate != null)
            {
                response = response.Where(predicate);
            }

            return response.AsEnumerable();
        }

        public TypiconEntity Get(Expression<Func<TypiconEntity, bool>> predicate)
        {
            return GetAll(predicate).FirstOrDefault();

            //if (typeof(DomainType).Equals(typeof(TypiconEntity)))
            //{
            //    //_typiconDBContext.TypiconEntitySet.Include(c => c.RulesFolder)
            //    //    .Include(c => c.CommonRules)
            //    //    .Include(c => c.MenologyRules)
            //    //    .Include(c => c.MenologyRules.Select(m => m.Day))
            //    //    .Include(c => c.TriodionRules)
            //    //    .Include(c => c.TriodionRules.Select(m => m.Day))
            //    //    .Include(c => c.ModifiedYears)
            //    //    .Include(c => c.Signs)
            //    //    .Where(predicate).FirstOrDefault();
            //    return _objectSet.
            //        Include("CommonRules").
            //        Include("MenologyRules").
            //        Include("MenologyRules.Date").
            //        Include("MenologyRules.DateB").
            //        Include("MenologyRules.DayRuleWorships").
            //        Include("TriodionRules").
            //        Include("TriodionRules.DayRuleWorships").
            //        Include("ModifiedYears").
            //        Include("ModifiedYears.ModifiedRules").
            //        Include("Signs").
            //        Include("Settings").
            //        Where(predicate).FirstOrDefault();
            //}

            //return _objectSet.Where(predicate).FirstOrDefault();
        }

        public void Update(TypiconEntity aggregate)
        {
            //_objectSet.Attach(aggregate);
            _typiconDBContext.Entry(aggregate).State = EntityState.Modified;
        }

        public void Insert(TypiconEntity aggregate)
        {
            _objectSet.Add(aggregate);
        }

        public void Delete(TypiconEntity aggregate)
        {
            _objectSet.Remove(aggregate);
        }
    }
}