using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllSignsQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<AllSignsQuery, IEnumerable<Sign>>
    {
        public AllSignsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        private readonly IncludeOptions Includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                "SignName"
            }
        };

        public IEnumerable<Sign> Handle([NotNull] AllSignsQuery query)
        {
            return UnitOfWork.Repository<Sign>().GetAll(c => c.OwnerId == query.TypiconId, Includes);
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
