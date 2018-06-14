using JetBrains.Annotations;
using System;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает День Октоиха по заданной дате
    /// </summary>
    public class OktoikhDayQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<OktoikhDayQuery, OktoikhDay>
    {
        public OktoikhDayQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor) 
            : base(unitOfWork, queryProcessor) { }

        public OktoikhDay Handle([NotNull] OktoikhDayQuery query)
        {
            int ihos = CalculateIhosNumber(query.Date);

            return UnitOfWork.Repository<OktoikhDay>().Get(c => c.Ihos == ihos && c.DayOfWeek == query.Date.DayOfWeek);
        }

        /// <summary>
        /// Вычисляет номер гласа по введенной дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private int CalculateIhosNumber(DateTime date)
        {
            DateTime dEaster = QueryProcessor.Process(new CurrentEasterQuery(date.Year));
            //Пасха в прошлом году
            DateTime dPastEaster = QueryProcessor.Process(new CurrentEasterQuery(date.Year - 1));

            //вычисляем количество дней между текущим днем и днем Пасхи
            int day = date.DayOfYear - dEaster.DayOfYear;

            //вычисляем глас
            // (остаток от деления на 56) / 7

            int ihos = day;
            if (day < 0)
            {
                //отталкиваемся от Пасхи в прошлом году

                //ниже получаем день относительно прошлой Пасхи

                ihos = 366 + date.DayOfYear - dPastEaster.DayOfYear;
            }
            ihos = (ihos % 56) / 7;
            if (ihos == 0)
                ihos = 8;


            return ihos;
        }
    }
}
