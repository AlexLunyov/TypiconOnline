using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Repository.EFSQLite
{
    /// <summary>
    /// Класс проходит все свойства ссылочного типа введенного объекта и добавляет их в выборку
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

            foreach (PropertyInfo propertyInfo in ((TypeInfo)typeof(DomainType)).DeclaredProperties)
            {
                if (propertyInfo.DeclaringType.IsClass)
                {
                    //если это класс - получаем его theninclude
                    //Expression<Func<DomainType, object>> propertyOfJoin;// = c => c = propertyInfo;
                    //request = request.GetIncludeThen(propertyOfJoin); // (propertyInfo.Name).ThenInclude(c => c.);
                }
            }

            return request;
        }

        private IQueryable<DomainType> GetIncludeThen(Expression<Func<DomainType, object>> includeProperty)
        {
            throw new NotImplementedException();
        }
    }
}
