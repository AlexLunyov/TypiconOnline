using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает наименование седмицы (вставляется в шапку шаблона седмицы)
    /// </summary>
    public class WeekNameQueryHandler : QueryStrategyHandlerBase, IQueryHandler<WeekNameQuery, ItemText>
    {
        public WeekNameQueryHandler(TypiconDBContext dbContext, IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        /// <summary>
        /// Возвращает наименование седмицы (вставляется в шапку шаблона седмицы)
        /// Примеры: Седмица 33-ая по Пятидесятнице
        ///          Седмица 6-ая по Пасхе
        ///          Седмица 3-ая Великого поста
        /// </summary>
        public ItemText Handle([NotNull] WeekNameQuery query)
        {
            /* Есть три периода: Великий пост, попразднество Пасхи и все после нее.
             * Соответсвенно, имена будут зависить от удаления от дня Пасхи.
             * Считаем, что date - это понедельник
             * -48 - Седмица n-ая Великого поста
             * -6 - Страстная седмица
             * 1 - Светлая седмица
             * 8 - n-ая по Пасхе
             * 56 - n-ая по Пятидесятнице
            */

            DateTime dEaster = QueryProcessor.Process(new CurrentEasterQuery(query.Date.Year));
            //Пасха в прошлом году
            DateTime dPastEaster = QueryProcessor.Process(new CurrentEasterQuery(query.Date.Year - 1));

            //Шаблоны из CommonRule

            var text = CalculateItemTextValue(query.Date, dEaster, dPastEaster, query.IsShortName);

            return text;
        }

        private ItemText CalculateItemTextValue(DateTime date, DateTime dEaster, DateTime dPastEaster, bool isShortName)
        {
            return new ItemText()
            {
                Items = new List<ItemTextUnit>()
                {
                    new ItemTextUnit("cs-ru", CalculateStringValue(date, dEaster, dPastEaster, isShortName))
                }
            };
        }

        private string CalculateStringValue(DateTime date, DateTime dEaster, DateTime dPastEaster, bool isShortName)
        {
            string result = "";

            //вычисляем количество жней между текущим днем и днем Пасхи
            int day = date.DayOfYear - dEaster.DayOfYear;
            int week;
            if ((day < -55) || (day > 49))
            {
                // n-ая по Пятидесятнице
                if (day < 0)
                {
                    day = 365;
                    //отталкиваемся от Пасхи в прошлом году
                    //ниже получаем день относительно прошлой Пасхи

                    day = day - dPastEaster.DayOfYear + date.DayOfYear;
                }

                week = (day - 43) / 7;

                if (isShortName)
                {
                    result = week.ToString() + " седм по Пятидесятнице";
                }
                else
                {
                    result = "Седмица " + week.ToString() + "-ая по Пятидесятнице";
                }
            }
            else if ((day >= -55) && (day < -48))
            {
                //Масленица
                if (isShortName)
                {
                    result = "Седмица сырная";
                }
                else
                {
                    result = "Седмица сырная (масленица)";
                }
            }
            else if ((day >= -48) && (day < -6))
            {
                // Великий пост

                week = (day + 55) / 7;
                if (isShortName)
                {
                    result = "Великий пост " + week.ToString();
                }
                else
                {
                    result = "Седмица " + week.ToString() + "-ая Великого поста";
                }
            }
            else if ((day >= -6) && (day < 1))
            {
                //Страстная
                result = "Страстная седмица";
            }
            else if ((day >= 1) && (day < 8))
            {
                // Светлая
                result = "Светлая седмица";
            }
            else if ((day >= 8) && (day <= 49))
            {
                //n - ая по Пасхе
                week = (day + 6) / 7;
                if (isShortName)
                {
                    result = week.ToString() + " седм по Пасхе";
                }
                else
                {
                    result = "Седмица " + week.ToString() + "-ая по Пасхе";
                }
            }

            return result;
        }
    }
}
