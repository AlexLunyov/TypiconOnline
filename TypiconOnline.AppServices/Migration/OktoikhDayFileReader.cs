﻿using System;
using System.Globalization;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Migration
{
    public class OktoikhDayFileReader : MigrationFileReaderBase, IOktoikhDayFileReader
    {
        public OktoikhDayFileReader(IFileReader fileReader) : base(fileReader) { }

        public string Read(int ihos, DayOfWeek dayOfWeek)
        {
            string result = _fileReader.Read(string.Format("{0}.{1}", ihos, dayOfWeek));

            if (string.IsNullOrEmpty(result))
            {
                //создаем фейковый объект
                result = CreateFakeXml(ihos, dayOfWeek);
            }
            
            return result;
        }

        private string CreateFakeXml(int ihos, DayOfWeek dayOfWeek)
        {
            /* Заполнены 3 файла:
             * [template].Monday.xml    - для седмичных дней
             * [template].Saturday.xml  - для субботних дней
             * [template].Sunday.xml    - для воскресных дней
            */

            if (dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
            {
                dayOfWeek = DayOfWeek.Monday;
            }

            string result = _fileReader.Read(string.Format("[template].{0}", dayOfWeek));

            string dayName = new CultureInfo("ru-RU").DateTimeFormat.DayNames[(int)dayOfWeek];

            result = result.Replace("[ihos]", string.Format("Глас {0}", ihos)).
                            Replace("[dayofweek]", dayName).
                            Replace(@"ihos=""1""", string.Format(@"ihos=""{0}""", ihos));

            return result;
        }
  }
}
