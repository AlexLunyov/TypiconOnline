using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllSignsQueryHandler : DbContextHandlerBase, IDataQueryHandler<AllSignsQuery, IEnumerable<Sign>>
    {
        public AllSignsQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        //private readonly IncludeOptions Includes = new IncludeOptions()
        //{
        //    Includes = new string[]
        //    {
        //        "SignName.Items"
        //    }
        //};

        public IEnumerable<Sign> Handle([NotNull] AllSignsQuery query)
        {
            return DbContext.Set<Sign>().Where(c => c.TypiconEntityId == query.TypiconId);
        }


        /// <summary>
        /// Возвращает конкретную дату в году, когда совершается данная служба
        /// </summary>
        /// <param name="year">Конкретный год</param>
        /// <returns>Если поля Date или DateB пустые, вовращает пустое (минимальное) значение</returns>
        //private string GetItemDateString(DateTime date)
        //{
        //    return new ItemDate(date.Month, date.Day).ToString();
        //}
    }
}
