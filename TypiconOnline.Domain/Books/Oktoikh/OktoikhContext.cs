using System;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Oktoikh
{
    /// <summary>
    /// Реализация IOktoikhContext, дающая доступ к богослужебным текстам Октоиха
    /// </summary>
    public class OktoikhContext : BookServiceBase, IOktoikhContext
    {
        IEasterContext easterContext;

        public OktoikhContext(IUnitOfWork unitOfWork, IEasterContext easterContext) : base(unitOfWork)
        {
            this.easterContext = easterContext ?? throw new ArgumentNullException("IEasterContext");
        }
        /// <summary>
        /// Возвращает текст богослужения дня и гласа, соответствующих заданной дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OktoikhDay Get(DateTime date)
        {
            int ihos = CalculateIhosNumber(date);

            return _unitOfWork.Repository<OktoikhDay>().Get(c => c.Ihos == ihos && c.DayOfWeek == date.DayOfWeek);
        }

        /// <summary>
        /// Вычисляет номер гласа по введенной дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int CalculateIhosNumber(DateTime date)
        {
            DateTime dEaster = easterContext.GetCurrentEaster(date.Year);
            //Пасха в прошлом году
            DateTime dPastEaster = easterContext.GetCurrentEaster(date.Year - 1);

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

        /// <summary>
        /// Возвращает строку с наименованием воскресного дня. 
        /// Используется для вставки в ряд воскресного дня в расписании на неделю
        /// </summary>
        /// <param name="date">Вводимая дата</param>
        /// <param name="language">Язык локализации</param>
        /// <param name="stringToPaste">Строка, которая будет вставлена после названия Недели, перед гласом</param>
        /// <returns>Возвращает строку с наименованием воскресного дня. </returns>
        public ItemTextUnit GetSundayName(DateTime date, string language, string stringToPaste = null)
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
            string result = "";

            //если этот день не воскресный, находим ближайшее воскресенье после него
            while (date.DayOfWeek != DayOfWeek.Sunday)
                date = date.AddDays(1);

            DateTime dEaster = easterContext.GetCurrentEaster(date.Year);
            //Пасха в прошлом году
            DateTime dPastEaster = easterContext.GetCurrentEaster(date.Year - 1);

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

            return new ItemTextUnit() { Language = language, Text = result };
        }

        /// <summary>
        /// Возвращает наименование седмицы (вставляется в шапку шаблона седмицы)
        /// Примеры: Седмица 33-ая по Пятидесятнице
        ///          Седмица 6-ая по Пасхе
        ///          Седмица 3-ая Великого поста
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <param name="isShortName">Если true, возвращает краткое название - для файлов.</param
        /// <returns></returns>
        public ItemTextUnit GetWeekName(DateTime date, string language, bool isShortName)
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
            string result = "";

            DateTime dEaster = easterContext.GetCurrentEaster(date.Year);

            //вычисляем количество жней между текущим днем и днем Пасхи
            int day = date.DayOfYear - dEaster.DayOfYear;
            int week = 0;
            if ((day < -55) || (day > 49))
            {
                // n-ая по Пятидесятнице
                if (day < 0)
                {
                    day = 365;
                    //отталкиваемся от Пасхи в прошлом году
                    //находим ее
                    dEaster = easterContext.GetCurrentEaster(date.Year - 1);
                    //ниже получаем день относительно прошлой Пасхи

                    day = day - dEaster.DayOfYear + date.DayOfYear;

                    week = (day - 43) / 7;
                }
                else
                {
                    week = (day - 43) / 7;
                }

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

            return new ItemTextUnit() { Language = language, Text = result };
        }
    }
}
