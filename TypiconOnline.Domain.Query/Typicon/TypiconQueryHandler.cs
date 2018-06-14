using JetBrains.Annotations;
using Mapster;
using System;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<TypiconQuery, TypiconEntityDTO>
    {
        public TypiconQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public TypiconEntityDTO Handle([NotNull] TypiconQuery query)
        {
            return UnitOfWork.Repository<TypiconEntity>().Get(c => c.Id == query.TypiconId).Adapt<TypiconEntityDTO>();
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
