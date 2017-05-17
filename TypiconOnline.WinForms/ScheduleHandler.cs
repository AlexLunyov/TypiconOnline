using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using TemplateEngine.Docx; //надо будет отказываться от этой пространства. оно оказалось бесполезным
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace ScheduleForm
{
    class ScheduleHandler
    {
        private ScheduleDBDataSet _ds = null;
        /// <summary>
        /// Результирующий документ Xml. Заполняется методом GetScheduleWeekXML
        /// </summary>
        private XmlDocument _xmlDoc = null;
        


        public ScheduleHandler(ScheduleDBDataSet dataSet)
        {
            _ds = dataSet;
        }

        /// <summary>
        /// Функция возвращает расписание на неделю
        /// </summary>
        /// <param name="date">Дата начала формирования недельного расписания. 
        /// Если это не понедельник, то за начало берется первый предыдущий понедельник.</param>
        /// <returns></returns>
        public XmlDocument GetScheduleWeekXML(DateTime date)
        {
            _xmlDoc = new XmlDocument();

            DateTime dEaster = GetEaster(date);

            // проверяем, понедельник ли вводимая дата. Если нет - отсчитываем назад до ближайшего понедельника
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            // проверяем, находится ли дата в периоде Триоди
            if (false)//(IsDateWithinTriodion(date, dEaster))
            {
                // внутри
            }
            else {
                // нет

                // выбираем из таблицы Mineinik 7 строк

                XmlNode scheduleNode = _xmlDoc.CreateElement("schedule");
                _xmlDoc.AppendChild(scheduleNode);
                XmlNode weeksNode = _xmlDoc.CreateElement("weeks");
                scheduleNode.AppendChild(weeksNode);

                XmlNode weekNode = _xmlDoc.CreateElement("week");
                weeksNode.AppendChild(weekNode);

                //добавляем наименование седмицы
                XmlNode nodeWeekName = _xmlDoc.CreateElement("name");
                nodeWeekName.InnerText = GetWeekName(date, false);
                weekNode.AppendChild(nodeWeekName);
                
                //nodes
                XmlNode daysNode = _xmlDoc.CreateElement("days");
                weekNode.AppendChild(daysNode);

                int i = 0;
                XmlNode curDayNode = null;
                XmlNode prevDayNode = null;
                while (i <= 7)
                {
                    //получаем ряд из таблицы Mineinik
                    ScheduleDBDataSet.MineinikRow mineinikRow = GetMineinikRow(date);
                    ScheduleDBDataSet.TriodionRow triodionRow = GetTriodionRow(date, dEaster);

                    //определим значения маркера hasMineaMarker
                    // 0 - Триоди нет
                    // 1 - name соединяем Минею с Триодью - не работает
                    // 2 - соединяем Триодь с Минеей
                    // 3 - только Триодь
                    int hasMineaMarker = 0;
                    if (triodionRow != null)
                    {
                        //hasMineaMarker = 1;
                        //if ((triodionRow.HasMinea == 4) || (triodionRow.HasMinea == mineinikRow.SignID))
                        //{
                        hasMineaMarker = 2;
                        //}
                        if ((triodionRow.HasMinea == 0) || (triodionRow.HasMinea > mineinikRow.SignID))
                        {
                            hasMineaMarker = 3;
                        } 
                    }

                    if (i < 7)
                    {
                        //обрабатываем все дни седмицы
                        // Обработка ряда

                        //создаем элементы xml
                        curDayNode = _xmlDoc.CreateElement("day");
                        daysNode.AppendChild(curDayNode);

                        XmlNode nameNode = _xmlDoc.CreateElement("name");
                        XmlAttribute isNameBoldAttribute = _xmlDoc.CreateAttribute("isbold");
                        XmlNode dateNode = _xmlDoc.CreateElement("date");
                        XmlNode dayOfWeekNode = _xmlDoc.CreateElement("dayofweek");
                        XmlNode signNode = _xmlDoc.CreateElement("sign");
                        XmlNode signDescrNode = _xmlDoc.CreateElement("signdescription");
                        XmlNode servicesNode = _xmlDoc.CreateElement("services");

                        curDayNode.AppendChild(nameNode);
                        curDayNode.AppendChild(dateNode);
                        curDayNode.AppendChild(signNode);
                        curDayNode.AppendChild(signDescrNode);
                        curDayNode.AppendChild(dayOfWeekNode);
                        curDayNode.AppendChild(servicesNode);

                        //теперь заполняем их данными

                        switch (hasMineaMarker)
                        {
                            case 0:
                                //Триоди нет
                                nameNode.InnerText = mineinikRow.Name;
                                signNode.InnerText = mineinikRow.ServiceSignsRow.ID.ToString();
                                signDescrNode.InnerText = mineinikRow.ServiceSignsRow.Name;
                                break;
                            case 1:
                                //name соединяем Минею с Триодью - не работает
                                nameNode.InnerText = mineinikRow.Name + " " + triodionRow.Name;
                                signNode.InnerText = triodionRow.SignID.ToString();
                                break;
                            case 2:
                                //name соединяем Триодь с Минеей
                                nameNode.InnerText = triodionRow.Name + " " + mineinikRow.Name;
                                signNode.InnerText = triodionRow.SignID.ToString();

                                //добавляем атрибут жирного выделения
                                if (triodionRow.IsNameBold)
                                {
                                    isNameBoldAttribute.Value = triodionRow.IsNameBold.ToString();
                                    nameNode.Attributes.Append(isNameBoldAttribute);
                                }
                                break;
                            case 3:
                                //только Триодь
                                nameNode.InnerText = triodionRow.Name;
                                signNode.InnerText = triodionRow.SignID.ToString();

                                //добавляем атрибут жирного выделения
                                if (triodionRow.IsNameBold)
                                {
                                    isNameBoldAttribute.Value = triodionRow.IsNameBold.ToString();
                                    nameNode.Attributes.Append(isNameBoldAttribute);
                                }
                                break;
                        }

                        //nameNode.InnerText = mineinikRow.Name;

                        dateNode.InnerText = date.ToString("dd MMMM yyyy г.");

                        dayOfWeekNode.InnerText = date.ToString("dddd").ToUpper();

                        //signNode.InnerText = mineinikRow.ServiceSignsRow.ID.ToString();

                        //signDescrNode.InnerText = mineinikRow.ServiceSignsRow.Name;

                        //смотрим, есть, ли у ряда Mineinik или Triodion свои собственные службы
                        //Если есть, то рассматриваем только их
                        ScheduleDBDataSet.ServiceFeaturesRow[] features;
                        if (hasMineaMarker == 0)
                            features = GetSubjectServices(mineinikRow);
                        else
                            features = GetSubjectServices(triodionRow);

                        if (features.Length == 0)
                        {
                            //ничего не нашли
                            //Проверяем, не воскресенье ли этот день?
                            if (i == 6)
                            {
                                //жестко определяем службы воскресного дня и переписываем знак на воскресный день
                                //но для начала его надо найти
                                DataRow sundayRow = _ds.ServiceSigns.Select("ID = 9")[0];
                                features = GetSubjectServices(sundayRow);
                                signNode.InnerText = "9";

                                //дописываем в Имя номер недели и глас
                                nameNode.InnerText = GetCurrentSundayName(date) + " " + nameNode.InnerText;
                            }
                            else
                            {
                                //Не воскресенье.
                                //Тогда выбираем службы по знаку, связанные с элементами таблицы ServiceSigns
                                if (hasMineaMarker == 0)
                                    features = GetSubjectServices(mineinikRow.ServiceSignsRow);
                                else
                                    features = GetSubjectServices(triodionRow.ServiceSignsRow);
                            }
                        }
                        
                        if (features.Length > 0)
                        {
                            foreach (ScheduleDBDataSet.ServiceFeaturesRow feature in features)
                            {
                                //проверяем, не накануне ли службы
                                if (!feature.IsDayBefore)
                                {
                                    FillXmlServiceNode(servicesNode, feature);
                                }
                                else if (prevDayNode != null)
                                {
                                    //если да, то если это возможно добавляем службы на день раньше
                                    //prevDayNode["services"]
                                    FillXmlServiceNode(prevDayNode["services"], feature);
                                }
                            }
                        }
                        else
                        {
                            throw new DayWithoutServicesException("Ошибка БД: нет заданных служб для " + date.ToString("dd MMMM yyyy г."));
                        }

                        

                        date = date.AddDays(1);
                        i++;
                        prevDayNode = curDayNode;
                    }
                    else
                    {
                        //теперь можно добавить вечерние службы понедельника следующей седмицы к элементу воскресеного дня
                        XmlNode servicesNode = prevDayNode["services"];

                        //смотрим, есть, ли у ряда Mineinik свои собственные службы
                        //Если есть, то рассматриваем только их
                        ScheduleDBDataSet.ServiceFeaturesRow[] features = GetSubjectServices(mineinikRow);
                        if (features.Length == 0)
                        {
                            //ничего не нашли. Тогда выбираем службы по знаку, связанные с элементами таблицы ServiceSigns
                            features = GetSubjectServices(mineinikRow.ServiceSignsRow);
                        }

                        if (features.Length > 0)
                        {
                            foreach (ScheduleDBDataSet.ServiceFeaturesRow feature in features)
                            {
                                //проверяем, не накануне ли службы
                                if (feature.IsDayBefore)
                                {
                                    FillXmlServiceNode(servicesNode, feature);
                                }
                            }
                        }
                        else
                        {
                            throw new DayWithoutServicesException("Ошибка БД: нет заданных служб для " + date.ToString("dd MMMM yyyy г."));
                        }

                        prevDayNode.AppendChild(servicesNode);
                        i++;
                    }
                }

                

            }

            // предусмотреть пересечение с таблицей Modifications

            return _xmlDoc;
        }

        /// <summary>
        /// Возвращает ряд из таблицы Triodion выбранный по вводимой дате относительно Пасхи
        /// </summary>
        /// <param name="date">дата</param>
        /// <param name="dEaster">Пасха в текущем году</param>
        /// <returns></returns>
        private ScheduleDBDataSet.TriodionRow GetTriodionRow(DateTime date, DateTime dEaster)
        {
            int dayFromEaster = date.DayOfYear - dEaster.DayOfYear;
            string selectString = "DayFromEaster = " + dayFromEaster.ToString();

            ScheduleDBDataSet.TriodionRow[] result = (ScheduleDBDataSet.TriodionRow[])_ds.Triodion.Select(selectString);
            if ((result != null) && (result.Length > 0))
                return result[0];
            else
                return null;
        }

        /// <summary>
        /// Функция возвращает расписание на неделю в формате TemplateEngine.Docx.Content
        /// !!Не рабочая версия!!
        /// </summary>
        /// <param name="date">Дата начала формирования недельного расписания. 
        /// Если это не понедельник, то за начало берется первый предыдущий понедельник.</param>
        /// <returns></returns>
        /*public Content GetScheduleWeekDocxContent(DateTime date)
        {
            DateTime dEaster = GetEaster(date);

            // проверяем, понедельник ли вводимая дата. Если нет - отсчитываем назад до ближайшего понедельника
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            //добавляем наименование седмицы
            //также добавляем таблицу "daystable", которую ниже будем наполнять
            TableContent daysTable = new TableContent("daystable");

            //этот объект будем возвращать как результат работы функции
            Content docxDoc = new Content(new FieldContent("weekname", GetWeekName(date, false)), daysTable);

            // проверяем, находится ли дата в периоде Триоди
            if (IsDateWithinTriodion(date, dEaster))
            {
                // внутри
            }
            else
            {
                // нет

                // выбираем из таблицы Mineinik 7 строк
                int i = 0;
                TableRowContent curRowContent = null;
                TableRowContent prevRowContent = null;
                while (i <= 7)
                {
                    //получаем ряд из таблицы Mineinik
                    ScheduleDBDataSet.MineinikRow row = GetMineinikRow(date);

                    ////создаем элемент ряда и добавляем его к таблице daysTable
                    //TableRowContent rowContent = new TableRowContent();
                    

                    if (i < 7)
                    {
                        //обрабатываем все дни седмицы
                        // Обработка ряда
                        

                        //XmlNode nameNode = _xmlDoc.CreateElement("name");
                        //nameNode.InnerText = row.Name;
                        FieldContent fcName = new FieldContent("dayname", row.Name);

                        //XmlNode dateNode = _xmlDoc.CreateElement("date");
                        //XmlNode dayOfWeekNode = _xmlDoc.CreateElement("dayofweek");

                        //dateNode.InnerText = date.ToString("dd MMMM yyyy г.");
                        //dayOfWeekNode.InnerText = date.ToString("dddd").ToUpper();

                        FieldContent fcDayofweek = new FieldContent("dayofweek", date.ToString("dddd").ToUpper());
                        FieldContent fcDaydate = new FieldContent("daydate", date.ToString("dd MMMM yyyy г."));

                        //sign
                        //XmlNode signNode = _xmlDoc.CreateElement("sign");
                        //signNode.InnerText = row.ServiceSignsRow.ID.ToString();

                        FieldContent fcSign = null;

                        switch (row.ServiceSignsRow.ID)
                        {
                            case 2:
                                //служба шестиричная - добавляем элемент в черную зону
                                fcSign = new FieldContent("blacksign", "@");
                                break;
                            case 3:
                                //славословная
                                fcSign = new FieldContent("sign", "@");
                                break;
                            case 4:
                                //полиелей
                                fcSign = new FieldContent("sign", "$");
                                break;
                            case 5:
                                //бдение
                                fcSign = new FieldContent("sign", "%");
                                break;
                            case 6:
                                //бдение
                                fcSign = new FieldContent("sign", "^");
                                break;
                        }

                        //services

                        //XmlNode servicesNode = _xmlDoc.CreateElement("services");

                        ListContent fcServices = new ListContent("dayservices");

                        //смотрим, есть, ли у ряда Mineinik свои собственные службы
                        //Если есть, то рассматриваем только их
                        ScheduleDBDataSet.ServiceFeaturesRow[] features = GetSubjectServices(row);
                        if (features.Length == 0)
                        {
                            //ничего не нашли
                            //Проверяем, не воскресный ли этот день?
                            if (i == 6)
                            {
                                //жестко определяем службы воскресного дня
                                //но для начала его надо найти
                                DataRow sundayRow = _ds.ServiceSigns.Select("ID = 9")[0];
                                features = GetSubjectServices(sundayRow);
                            }
                            else
                            {
                                //Не воскресенье.
                                //Тогда выбираем службы по знаку, связанные с элементами таблицы ServiceSigns
                                features = GetSubjectServices(row.ServiceSignsRow);
                            }
                        }

                        if (features.Length > 0)
                        {
                            foreach (ScheduleDBDataSet.ServiceFeaturesRow feature in features)
                            {
                                //проверяем, не накануне ли службы
                                if (!feature.IsDayBefore)
                                {
                                    FillDocxServiceNode(fcServices, feature);
                                }
                                else if (prevRowContent != null)
                                {
                                    //если да, то если это возможно добавляем службы на день раньше
                                    //prevDayNode["services"]

                                    //prevRowContent.Fiel_ds
                                    ListContent tryy = new ListContent();
                                    FillDocxServiceNode(tryy, feature);
                                }
                            }
                        }
                        else
                        {
                            throw new DayWithoutServicesException("Ошибка БД: нет заданных служб для " + date.ToString("dd MMMM yyyy г."));
                        }



                        //curDayNode.AppendChild(nameNode);
                        //curDayNode.AppendChild(dateNode);
                        //curDayNode.AppendChild(signNode);
                        //curDayNode.AppendChild(signDescrNode);
                        //curDayNode.AppendChild(dayOfWeekNode);
                        //curDayNode.AppendChild(servicesNode);

                        //добавляем все элементы в ряд выходной таблицы
                        daysTable.AddRow(fcName, fcDaydate, fcSign, fcDayofweek, fcServices);
                        //curRowContent = daysTable.Rows.Last;

                        date = date.AddDays(1);
                        i++;
                        prevRowContent = daysTable.Rows.Last();
                    }
                    else
                    {
                        //это уже понедельник следующей седмицы
                        //теперь можно добавить вечерние службы понедельника следующей седмицы к элементу воскресеного дня
                        //XmlNode servicesNode = prevDayNode["services"];

                        //смотрим, есть, ли у ряда Mineinik свои собственные службы
                        //Если есть, то рассматриваем только их
                        ScheduleDBDataSet.ServiceFeaturesRow[] features = GetSubjectServices(row);
                        if (features.Length == 0)
                        {
                            //ничего не нашли. Тогда выбираем службы по знаку, связанные с элементами таблицы ServiceSigns
                            features = GetSubjectServices(row.ServiceSignsRow);
                        }

                        if (features.Length > 0)
                        {
                            foreach (ScheduleDBDataSet.ServiceFeaturesRow feature in features)
                            {
                                //проверяем, не накануне ли службы
                                if (feature.IsDayBefore)
                                {
                                    //prevRowContent.Fiel_ds
                                    ListContent tryy = new ListContent();
                                    FillDocxServiceNode(tryy, feature);
                                }
                            }
                        }
                        else
                        {
                            throw new DayWithoutServicesException("Ошибка БД: нет заданных служб для " + date.ToString("dd MMMM yyyy г."));
                        }

                        //prevDayNode.AppendChild(servicesNode);
                        i++;
                    }
                }



            }

            // предусмотреть пересечение с таблицей Modifications

            return docxDoc;
        }*/

        /// <summary>
        /// Функция возвращает ряд из таблицы Mineinik выбранный по вводимой дате, без привязки к году. Только день и месяц
        /// Проверка на високосный год заложена здесь
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private ScheduleDBDataSet.MineinikRow GetMineinikRow(DateTime date)
        {
            string selectString = "substring(convert(";

            // проверяем, високосный ли год?
            if (DateTime.IsLeapYear(date.Year))
            {
                // да - тогда выбирваем из колонки DateB
                selectString += "DateB";
            } else
            {
                // нет - из колонки Date
                selectString += "Date";
            }
            selectString += ", 'System.String'), 1,5) like '" + date.ToString("dd.MM") + "'";

            ScheduleDBDataSet.MineinikRow[] result = (ScheduleDBDataSet.MineinikRow[]) _ds.Mineinik.Select(selectString);
            if ((result != null) && (result.Length > 0))
                return result[0];
            else {
                throw new MineinikNotFoundException("Ошибка БД: Служба не назначена на " + date.ToString("dd mmmm."));
            }

            //return null;
        }

        /// <summary>
        /// Возвращает ID предустановленного шаблона.
        /// </summary>
        /// <param name="sign">Вводимый ID для проверки</param>
        /// <returns></returns>
        private int GetTemplateSignID(int sign)
        {
            int templateSignID = 0;

            string selectString = "ID = " + sign.ToString();
            ScheduleDBDataSet.ServiceSignsRow[] serviceSignsRow  = (ScheduleDBDataSet.ServiceSignsRow[]) _ds.ServiceSigns.Select(selectString);

            if (serviceSignsRow.Length > 0)
            {
                if (serviceSignsRow[0].IsTemplate)
                    templateSignID = serviceSignsRow[0].ID;
                else
                    templateSignID = serviceSignsRow[0].TemplateID;
            }

            return templateSignID;
        }

        /// <summary>
        /// Функция определяет, входит ли данная дата в период Триоди
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <param name="dEaster">Пасха в исчисляемом году</param>
        /// <returns></returns>
        public Boolean IsDateWithinTriodion(DateTime date, DateTime dEaster)
        {
            //находим в таблице Triodion самые крайние значение вокруг Пасхи
            System.Data.DataRow[] drTr = _ds.Triodion.Select("", "DayFromEaster ASC");
            if ((drTr != null) && (drTr.Length != 0)) { 
                double first = (double)drTr[0]["DayFromEaster"];
                double last = (double)drTr[drTr.Length - 1]["DayFromEaster"];

                //сравниваем
                DateTime dFirst = dEaster.AddDays(first);
                DateTime dLast = dEaster.AddDays(last);

                if ((date.DayOfYear >= dFirst.DayOfYear) & (date.DayOfYear <= dLast.DayOfYear))
                {
                    return true;
                }                
            }
            // во всех остальных случаях возвращаем false
            return false;
        }

        /// <summary>
        /// Возвращает наименование седмицы (вставляется в шапку шаблона)
        /// Примеры: Седмица 33-ая по Пятидесятнице
        ///          Седмица 6-ая по Пасхе
        ///          Седмица 3-ая Великого поста
        /// </summary>
        /// <param name="date">Дата для проверки</param>
        /// <param name="isShortName">Если true, возвращает краткое название - для файлов.</param
        /// <returns></returns>
        public string GetWeekName(DateTime date, Boolean isShortName)
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

            DateTime dEaster = GetEaster(date);

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
                    dEaster = GetEaster(date.AddYears(-1));
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
            else
            {
                if ((day >= -55) && (day < -48))
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
                    if (isShortName) {
                        result = "Великий пост " + week.ToString();
                    }
                    else
                    {
                        result = "Седмица "+ week.ToString() + "-ая Великого поста";
                    }
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
                        if ((day >= 1) && (day < 8))
                        {
                            // Светлая
                            result = "Светлая седмица";
                        }
                        else
                        {
                            if ((day >= 8) && (day <= 49))
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
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает строку с наименованием воскресного дня. 
        /// Используется для вставки в ряд воскресного дня в расписании на неделю
        /// </summary>
        /// <param name="date">Вводимая дата</param>
        /// <returns>Возвращает строку с наименованием воскресного дня. </returns>
        public string GetCurrentSundayName(DateTime date)
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

            //если этот день не воскресныйЮ находим ближайшее воскресенье после него
            while (date.DayOfWeek != DayOfWeek.Sunday)
                date = date.AddDays(1);

            DateTime dEaster = GetEaster(date);
            //Пасха в прошлом году
            DateTime dPastEaster = GetEaster(date.AddYears(-1));

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

            if ((day < -55) || (day > 49))
            {
                // n-ая по Пятидесятнице
                if (day < 0)
                {
                    //отталкиваемся от Пасхи в прошлом году
                    
                    //ниже получаем день относительно прошлой Пасхи

                    day = 365 - dPastEaster.DayOfYear + date.DayOfYear;
                }
                week = (day - 43) / 7;

                result = "Неделя " + week.ToString() + "-ая по Пятидесятнице. Глас " + glas.ToString() + "-й.";
            }
            //остальные варианты не рассматриваем - они должны быть прописаны в элементах таблицы Triodion
            //возвращаем только глас, и то не везде
            else
            {
                if ((day >= -55) && (day < -48))
                {
                    //Масленица
                    //result = "Седмица сырная (масленица)";
                }
                else if ((day >= -48) && (day < -6))
                {
                    // Великий пост

                    result = " Глас " + glas.ToString() + "-й.";
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
                        if ((day >= 1) && (day < 8))
                        {
                            // Светлая
                            result = "Светлая седмица";
                        }
                        else
                        {
                            if ((day >= 8) && (day <= 49))
                            {
                                //n - ая по Пасхе
                                week = (day + 6) / 7;
                                result = "Седмица " + week.ToString() + "-ая по Пасхе";
                            }
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает дату Пасхи года вводимой даты date
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns></returns>
        public DateTime GetEaster(DateTime date)
        {
            //Находим в таблице Paskhalia текущую дату Пасхи
            string exp = "Date > '01-01-" + date.Year.ToString() + "'";
            System.Data.DataRow[] drP = _ds.Paskhalia.Select(exp, "date asc");
            if ((drP != null) && (drP.Length >= 1))
            {
                return (DateTime)drP[0]["Date"];
            }
            else
            {
                throw new EasterNotFoundException("Ошибка БД: не задана дата Пасхи на " + date.Year.ToString() + "год.");
            }
        }

        /// <summary>
        /// Возвращает коллекцию строк служб, относящихся к элементу таблиц Mineinik, Triodion или ServiSign
        /// </summary>
        /// <param name="row">Строка таблиц таблиц Mineinik, Triodion или ServiSign</param>
        /// <returns></returns>
        public ScheduleDBDataSet.ServiceFeaturesRow[] GetSubjectServices(DataRow row)
        {
            //образец строки фильтрации
            // _ds.ServiceFeatures.Select("(Kind = 0) AND (ParentID = 5)", "time asc")
            string selectString = "(Kind = ";

            switch (row.Table.TableName)
            {
                case "ServiceSigns":
                    selectString += "0";
                    break;
                case "Modifications":
                    selectString += "1";
                    break;
                case "Mineinik":
                    selectString += "2";
                    break;
                case "Triodion":
                    selectString += "3";
                    break;
            }
            selectString += ") AND(ParentID = " + row["ID"].ToString() + ")";

            DataRow[] result = _ds.ServiceFeatures.Select(selectString, "time asc");

            return (ScheduleDBDataSet.ServiceFeaturesRow[]) result;
        }

        /// <summary>
        /// Заполняет указанный элемент xml "services" данными о службе, содержащимися в строке таблицы ServiceFeatures
        /// </summary>
        /// <param name="node"></param>
        /// <param name="prevDayNode"></param>
        /// <param name="row"></param>
        private XmlNode GetXmlServicesNodeFromMinea(XmlNode curDayNode, XmlNode prevDayNode, ScheduleDBDataSet.MineinikRow row)
        {
            XmlNode resultNode = curDayNode.OwnerDocument.CreateElement("services");
            //смотрим, есть, ли у ряда Mineinik свои собственные службы
            //Если есть, то рассматриваем только их
            ScheduleDBDataSet.ServiceFeaturesRow[] features = GetSubjectServices(row);
            if (features.Length == 0)
            {
                //ничего не нашли. Тогда выбираем службы по знаку
                features = GetSubjectServices(row.ServiceSignsRow);
            }

            if (features.Length > 0)
            {
                foreach (ScheduleDBDataSet.ServiceFeaturesRow feature in features)
                {
                    //проверяем, не накануне ли службы
                    if (!feature.IsDayBefore)
                    {
                        FillXmlServiceNode(resultNode, feature);
                    }
                    else if (prevDayNode != null)
                    {
                        //если да, то если это возможно добавляем службы на день раньше
                        //prevDayNode["services"]
                        FillXmlServiceNode(prevDayNode["services"], feature);
                    }
                }
            }
            else
            {
                throw new DayWithoutServicesException("Ошибка БД: нет заданных служб для " + row.Date.ToString("dd MMMM yyyy г."));
            }
            return resultNode;
        }

        /// <summary>
        /// Заполняет указанный элемент xml данными о службе, содержащимися в строке таблицы ServiceFeatures
        /// </summary>
        /// <param name="node">Xml элемент "services" для заполнения</param>
        /// <param name="row">строка таблицы ServiceFeatures</param>
        private void FillXmlServiceNode(XmlNode node, ScheduleDBDataSet.ServiceFeaturesRow row)
        {
            XmlNode serviceNode = node.OwnerDocument.CreateElement("service");
            //атрибуты

            if (row.IsTimeBold)
            {
                XmlAttribute istimeboldAttr = node.OwnerDocument.CreateAttribute("istimebold");
                istimeboldAttr.Value = row.IsTimeBold.ToString();
                serviceNode.Attributes.Append(istimeboldAttr);
            }

            if (row.IsTimeRed)
            {
                XmlAttribute istimeredAttr = node.OwnerDocument.CreateAttribute("istimered");
                istimeredAttr.Value = row.IsTimeRed.ToString();
                serviceNode.Attributes.Append(istimeredAttr);
            }

            if (row.IsNameBold)
            {
                XmlAttribute isnameboldAttr = node.OwnerDocument.CreateAttribute("isnamebold");
                isnameboldAttr.Value = row.IsNameBold.ToString();
                serviceNode.Attributes.Append(isnameboldAttr);
            }

            if (row.IsNameRed)
            {
                XmlAttribute isnameredAttr = node.OwnerDocument.CreateAttribute("isnamered");
                isnameredAttr.Value = row.IsNameRed.ToString();
                serviceNode.Attributes.Append(isnameredAttr);
            }

            XmlNode serviceTimeNode = node.OwnerDocument.CreateElement("time");
            serviceTimeNode.InnerText = row.Time;
            XmlNode serviceNameNode = node.OwnerDocument.CreateElement("name");
            serviceNameNode.InnerText = row.Name;

            if (row.AdditionalName != "")
            {
                XmlNode serviceAdditionalNameNode = node.OwnerDocument.CreateElement("additionalname");
                serviceAdditionalNameNode.InnerText = row.AdditionalName;
                serviceNode.AppendChild(serviceAdditionalNameNode);
            }

            serviceNode.AppendChild(serviceTimeNode);
            serviceNode.AppendChild(serviceNameNode);

            node.AppendChild(serviceNode);
        }

        /// <summary>
        /// Заполняет Docx шаблон данными сформированного ранее расписания.
        /// Примечание: заполняется только первая неделя xml-документа
        /// <param name="fileName">Имя файла Docx шаблона. Результат записывается в него же.</param>
        /// </summary>
        /// <returns>Возвращает строку. Если пустая - значит все прошло успешно. Иначе - сообщение возникшего исключения</returns>
        public string FillDocxTemplate(string fileName)
        {
            string resultString = "";
            if (_xmlDoc != null)
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(fileName, true))
                {
                    if (true)/*try*/
                    {
                        //просматриваем все элементы ChildElements и выбираем только таблицы
                        //считаем, что всего таблиц 10. 
                        //0 - шапка с названием седмицы, а остальные - шаблоны для обозначения знаков служб

                        //эта коллекция будет вместилищем таблиц - docx-шаблонов
                        var templateTables = new List<Table>();
                        //ищем их все и добавляем в коллекцию
                        foreach (OpenXmlElement element in doc.MainDocumentPart.Document.Body.ChildElements)
                        {
                            if (element.GetType() == typeof(Table))
                                templateTables.Add((Table)element);
                        }

                        //создаем коллекцию таблиц, которые будут результирующим содержанием выходного документа
                        List<OpenXmlElement> resultElements = new List<OpenXmlElement>();

                        //шапка
                        Table headerTable = templateTables[0];

                        //теперь надо парсить _xmlDoc и вставлять в docX
                        XmlNodeList weekNodes = _xmlDoc.GetElementsByTagName("week");
                    
                        XmlNode weekNode = weekNodes[0];
                        //Название седмицы
                        //table[2]->tr[1]->td[1]->p[1]->r[1]->t[1]
                        TableCell tdWeekName = (TableCell)headerTable.ChildElements[2].ChildElements[1];
                        string sWeekName = weekNode.SelectSingleNode("name").InnerText;
                        SetTextToCell(tdWeekName, sWeekName, false, false);
                        resultElements.Add(headerTable);

                        //теперь начинаем наполнять дни
                        XmlNode daysNode = weekNode.SelectSingleNode("days");
                        foreach (XmlNode dayNode in daysNode.ChildNodes)
                        {
                            Table dayTable = new Table();
                            int sign = GetTemplateSignID(Convert.ToInt32(dayNode.SelectSingleNode("sign").InnerText));
                            //в зависимости от того, какой знак дня - берем для заполнения шаблона соответствующую таблицу в templateTables
                            Table dayTemplateTable = templateTables[sign];

                            TableRow tr = (TableRow)dayTemplateTable.ChildElements[2].Clone();
                            TableCell tdDayofweek = (TableCell)tr.ChildElements[2];
                            TableCell tdName = (TableCell)tr.ChildElements[3];
                            string sDayofweek = dayNode.SelectSingleNode("dayofweek").InnerText;
                            string sName = dayNode.SelectSingleNode("name").InnerText;
                            Boolean bIsNameBold = (dayNode.SelectSingleNode("name").Attributes["isbold"] != null);

                            SetTextToCell(tdDayofweek, sDayofweek, false, false);
                            SetTextToCell(tdName, sName, bIsNameBold, false);
                            //tr.ChildElements[2].InnerXml = dayNode.SelectSingleNode("dayofweek").InnerText;
                            //tr.ChildElements[3].InnerXml = dayNode.SelectSingleNode("name").InnerText;
                            dayTable.AppendChild(tr);

                            tr = (TableRow)dayTemplateTable.ChildElements[3].Clone();
                            TableCell tdDate = (TableCell)tr.ChildElements[2];
                            string sDate = dayNode.SelectSingleNode("date").InnerText;
                            SetTextToCell(tdDate, sDate, false, false);
                            //tr.ChildElements[2].InnerXml = dayNode.SelectSingleNode("date").InnerText;
                            dayTable.AppendChild(tr);

                            foreach (XmlNode serviceNode in dayNode.SelectSingleNode("services").ChildNodes)
                            {
                                tr = (TableRow)dayTemplateTable.ChildElements[4].Clone();
                                TableCell tdTime = (TableCell)tr.ChildElements[2];
                                TableCell tdSName = (TableCell)tr.ChildElements[3];

                                XmlNode nodeTime = serviceNode.SelectSingleNode("time");
                                XmlNode nodeName = serviceNode.SelectSingleNode("name");
                                string sTime = nodeTime.InnerText;
                                string sSName = nodeName.InnerText;

                                Boolean bIsTimeBold = (serviceNode.Attributes["istimebold"] != null);
                                Boolean bIsTimeRed = (serviceNode.Attributes["istimered"] != null);

                                Boolean bIsServiceNameBold = (serviceNode.Attributes["isnamebold"] != null);
                                Boolean bIsServiceNameRed = (serviceNode.Attributes["isnamered"] != null);


                                SetTextToCell(tdTime, sTime, bIsTimeBold, bIsTimeRed);
                                SetTextToCell(tdSName, sSName, bIsServiceNameBold, bIsServiceNameRed);

                                //additionalName
                                XmlNode nodeAddName = serviceNode.SelectSingleNode("additionalname");
                                if (nodeAddName != null)
                                {
                                    string sAddName = nodeAddName.InnerText;
                                    AppendTextToCell(tdSName, sAddName, true, false);
                                }

                                //tr.ChildElements[1].InnerXml = dayNode.SelectSingleNode("time").InnerText;
                                //tr.ChildElements[2].InnerXml = dayNode.SelectSingleNode("name").InnerText;
                                dayTable.AppendChild(tr);
                            }
                            //добавляем таблицу к выходной коллекции
                            resultElements.Add(dayTable);
                        }

                        //в конце удаляем все из документа, оставляя только колонтитулы (прописаны в SectionProperties) и результирующие таблицы

                        foreach (OpenXmlElement el in doc.MainDocumentPart.Document.Body.ChildElements)
                        {
                            if (el.GetType() == typeof(SectionProperties))
                            {
                                resultElements.Add(el);
                                break;
                            }
                        }

                        doc.MainDocumentPart.Document.Body.RemoveAllChildren();
                        foreach (OpenXmlElement t in resultElements)
                        {
                            doc.MainDocumentPart.Document.Body.AppendChild(t);
                        }

                        doc.MainDocumentPart.Document.Save();
                    }
                    /*catch (XmlException xmlEx)
                    {
                        resultString = "Ошибка Xml документа. Неправильно составлен Xml документ. " + xmlEx.Message;
                    }
                    catch (Exception ex)
                    {
                        resultString = "При выполнении функции возникла ошибка: 0" + ex.Message;
                    }*/
                }
            } else
            {
                resultString = "Сначала воспользуйтесь методом GetScheduleWeekXML!";
            }

            return resultString;
        }

        /// <summary>
        /// Функция задает строчное значение text для параграфа
        /// </summary>
        /// <param name="td"></param>
        /// <param name="text"></param>
        private void SetTextToCell(TableCell td, string text, Boolean isBold, Boolean isRed)
        {
            //выбираем первый элемент run
            //удаляем все остальные
            //задаем в элементе text нужное значение
            Paragraph p = (Paragraph) td.ChildElements[1];

            Run r = null;
            //ищем первый попавшийся элемент run в параграфе
            foreach (OpenXmlElement element in p.ChildElements)
            {
                if (element.GetType() == typeof(Run))
                {
                    r = (Run)element.Clone();
                    break;
                }
            }
            //RunStyle

            RunProperties properties = null;

            if (isBold || isRed)
            {
                properties = (RunProperties)r.ChildElements[0];//.Clone();
            }

            if (isBold)
            {
                properties.Bold = new Bold();
            }

            if (isRed)
            {
                properties.Color = new Color() { Val = "FF0000" };
            }

            Text t = (Text) r.ChildElements[1];
            t.Text = text;

            p.RemoveAllChildren<Run>();
            p.Append(r);
        }

        /// <summary>
        /// Функция добавляет строчное значение text для параграфа
        /// </summary>
        /// <param name="td"></param>
        /// <param name="text"></param>
        /// <param name="isBold"></param>
        /// <param name="isRed"></param>
        private void AppendTextToCell(TableCell td, string text, Boolean isBold, Boolean isRed)
        {
            //выбираем первый элемент run
            //удаляем все остальные
            //задаем в элементе text нужное значение
            Paragraph p = (Paragraph)td.ChildElements[1];

            Run r = null;
            //ищем первый попавшийся элемент run в параграфе
            foreach (OpenXmlElement element in p.ChildElements)
            {
                if (element.GetType() == typeof(Run))
                {
                    r = (Run)element.Clone();
                    break;
                }
            }

            //добавяляем пробел. Другого способа не нашел, как добавить элемент run
            //не работает

            //Run runSpace = (Run) r.Clone();
            //Text textSpace = (Text)runSpace.ChildElements[1];
            //textSpace.Text = " ";
            //p.Append(runSpace);

            //RunStyle

            RunProperties properties = null;

            if (isBold || isRed)
            {
                properties = (RunProperties)r.ChildElements[0];//.Clone();
            }

            if (isBold)
            {
                properties.Bold = new Bold();
            }
            else
                properties.Bold = null;

            if (isRed)
            {
                properties.Color = new Color() { Val = "FF0000" };
            }
            else
            {
                properties.Color = null;
            }

            Text t = (Text)r.ChildElements[1];
            t.Text = " " + text;
            //настройка, чтобы не резал пробелы
            t.Space = SpaceProcessingModeValues.Preserve;

            p.Append(r);
        }

        /// <summary>
        /// Сохраняет в файл текстовую форму расписания на неделю.
        /// Она должна быть соформирована через метод GetScheduleWeekXML
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Расписание</returns>
        public string FillTextTemplate(string fileName)
        {
            string resultString = "";
            if (_xmlDoc != null)
            {
                if (true)/*try*/
                {
                    //теперь надо парсить _xmlDoc и вставлять в docX
                    XmlNodeList weekNodes = _xmlDoc.GetElementsByTagName("week");

                    XmlNode weekNode = weekNodes[0];
                    //Название седмицы пропускаем. Считаем, что название седмицы будет в наименовании файла

                    //теперь начинаем наполнять дни
                    XmlNode daysNode = weekNode.SelectSingleNode("days");
                    foreach (XmlNode dayNode in daysNode.ChildNodes)
                    {
                        resultString += "<div style=\"margin - top:10px; \">";

                        int sign = GetTemplateSignID(Convert.ToInt32(dayNode.SelectSingleNode("sign").InnerText));

                        resultString += "[sign cat=\"" + sign.ToString() + "\"]<strong>";

                        //если бдение или бдение с литией или воскресный день - красим в красный цвет
                        if (sign == 5 || sign == 6 || sign == 9)
                            resultString += "<span style=\"color: #ff0000;\">";

                        resultString += dayNode.SelectSingleNode("date").InnerText + "<br>";
                        resultString += dayNode.SelectSingleNode("dayofweek").InnerText + "<br>";
                        resultString += dayNode.SelectSingleNode("name").InnerText + "</strong>";

                        if (sign == 5 || sign == 6 || sign == 9)
                            resultString += "</span>";

                        resultString += "</div>";

                        resultString += "<table border=0>";

                        foreach (XmlNode serviceNode in dayNode.SelectSingleNode("services").ChildNodes)
                        {
                            resultString += "<tr><td>";

                            resultString += serviceNode.SelectSingleNode("time").InnerText + "&nbsp;</td><td>";
                            resultString += serviceNode.SelectSingleNode("name").InnerText;

                            //additionalName
                            XmlNode nodeAddName = serviceNode.SelectSingleNode("additionalname");
                            if (nodeAddName != null)
                            {
                                resultString += "<strong>" + nodeAddName.InnerText + "</strong>";
                            }
                            resultString += "</td></tr>";
                        }

                        resultString += "</table>";
                    }
                }
                /*catch (XmlException xmlEx)
                {
                    resultString = "Ошибка Xml документа. Неправильно составлен Xml документ. " + xmlEx.Message;
                }
                catch (Exception ex)
                {
                    resultString = "При выполнении функции возникла ошибка: 0" + ex.Message;
                }*/

                //записываем все в файл
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);

                System.IO.File.WriteAllText(fileName, resultString);

            }
            else
            {
                resultString = "Сначала воспользуйтесь методом GetScheduleWeekXML!";
            }

            return resultString;
        }
    }
}
