using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.AppServices.Migration
{
    public class PrintTemplateImporter
    {
        readonly SimpleFileReader _fileReader;
        public PrintTemplateImporter(string folder)
        {
            _fileReader = new SimpleFileReader(folder);
        }

        public PrintDayTemplate GetDay(int number, int? icon, bool isRed, string name, TypiconVersion version)
        {
            var fileName = $"{number.ToString()}.docx";
            var file = _fileReader.ReadBinary(fileName);

            return new PrintDayTemplate()
            {
                Name = name,
                Number = number,
                Icon = icon,
                IsRed = isRed,
                PrintFile = file,
                PrintFileName = fileName,
                TypiconVersion = version
            };
        }

        public PrintWeekTemplate GetWeek(TypiconVersion version)
        {
            var fileName = "week.docx";
            var file = _fileReader.ReadBinary(fileName);

            return new PrintWeekTemplate()
            {
                DaysPerPage = 4,
                PrintFile = file,
                PrintFileName = fileName,
                TypiconVersion = version
            };
        }
    }
}
