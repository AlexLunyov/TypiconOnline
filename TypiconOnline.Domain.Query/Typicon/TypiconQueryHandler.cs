using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class TypiconQueryHandler : DbContextHandlerBase, IDataQueryHandler<TypiconQuery, TypiconEntityDTO>
    {
        public TypiconQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public TypiconEntityDTO Handle([NotNull] TypiconQuery query)
        {
            return DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == query.TypiconId).Adapt<TypiconEntityDTO>();
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
