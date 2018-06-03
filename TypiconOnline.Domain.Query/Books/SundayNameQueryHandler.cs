using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Exceptions;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает День Октоиха по заданной дате
    /// </summary>
    public class SundayNameQueryHandler : QueryStrategyHandlerBase, IQueryHandler<SundayNameQuery, ItemTextUnit>
    {
        public SundayNameQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        /// <summary>
        /// Возвращает День Октоиха по заданной дате
        /// </summary>
        public ItemTextUnit Handle([NotNull] SundayNameQuery query)
        {
            /* Есть три периода: Великий пост, попразднество Пасхи и все после нее.
             * Соответсвенно, имена будут зависить от удаления от дня Пасхи.
             * Считаем, что date - это понедельник
             * -70 - Подготовительные недели к Великому Посту
             * -48 - Седмица n-ая Великого поста
             * -6 - Страстная седмица
             * 1 - Светлая седмица
             * 8 - n-ая по Пасхе
             * 56 - n-ая по Пятидесятнице
            */

            var date = query.Date;

            //если этот день не воскресный, находим ближайшее воскресенье после него
            while (date.DayOfWeek != DayOfWeek.Sunday)
                date = date.AddDays(1);

            DateTime dEaster = QueryProcessor.Process(new CurrentEasterQuery(date.Year));
            //Пасха в прошлом году
            DateTime dPastEaster = QueryProcessor.Process(new CurrentEasterQuery(date.Year - 1));

            string text = CalculateStringValue(date, dEaster, dPastEaster, query.StringToPaste);

            return new ItemTextUnit() { Language = query.Language, Text = text };
        }

        private string CalculateStringValue(DateTime date, DateTime dEaster, DateTime dPastEaster, string stringToPaste)
        {
            string result = "";

            //вычисляем количество дней между текущим днем и днем Пасхи
            int day = date.DayOfYear - dEaster.DayOfYear;
            int week = 0;

            //вычисляем глас
            // (остаток от деления на 56) / 7

            int glas = day;
            if (day < 0)
            {
                //отталкиваемся от Пасхи в прошлом году

                //ниже получаем день относительно прошлой Пасхи

                glas = 366 + date.DayOfYear - dPastEaster.DayOfYear;
            }
            glas = (glas % 56) / 7;
            if (glas == 0)
                glas = 8;

            //
            if ((day < -70) || (day > 63))//(day > 49))
            {
                // n-ая по Пятидесятнице
                if (day < 0)
                {
                    //отталкиваемся от Пасхи в прошлом году

                    //ниже получаем день относительно прошлой Пасхи

                    day = 365 - dPastEaster.DayOfYear + date.DayOfYear;
                }
                week = (day - 43) / 7;

                result = "Неделя " + week.ToString() + "-ая по Пятидесятнице";

                if (!string.IsNullOrEmpty(stringToPaste))
                {
                    result += ", " + stringToPaste;
                }

                result += ". Глас " + glas.ToString() + "-й.";
            }
            //остальные варианты не рассматриваем - они должны быть прописаны в элементах таблицы Triodion
            //возвращаем только глас, и то не везде
            else
            {
                if ((day >= -70) && (day < -55))
                {
                    //Подготовительные недели к Великому Посту
                    if (!string.IsNullOrEmpty(stringToPaste))
                    {
                        result = $"{stringToPaste} ";
                    }

                    result += $"Глас {glas}-й.";
                }
                else if ((day >= -55) && (day < -48))
                {
                    //Масленица
                    //result = "Седмица сырная (масленица)";
                }
                else if ((day >= -48) && (day < -6))
                {
                    // Великий пост

                    if (!string.IsNullOrEmpty(stringToPaste))
                    {
                        result = $"{stringToPaste} ";
                    }

                    result += "Глас " + glas.ToString() + "-й.";
                    //week = (day + 55) / 7;
                    //result = "Седмица " + week.ToString() + "-ая Великого поста";
                }
                else
                {
                    if ((day >= -6) && (day < 1))
                    {
                        //Страстная
                        result = "Страстная седмица";
                    }
                    else
                    {
                        //if ((day >= 1) && (day < 8))
                        //{
                        //    // Светлая
                        //    result = "Светлая седмица";
                        //}
                        //else
                        //{
                        //if ((day >= 8) && (day <= 49))
                        //{
                        //    //n - ая по Пасхе
                        //    week = (day + 6) / 7;
                        //    result = "Неделя " + week.ToString() + "-ая по Пасхе";
                        //}
                        //}
                    }
                }
            }

            return result;
        }
    }
}
