using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreatePrintWeekTemplateCommand : CreateRuleCommandBase<PrintWeekTemplate>
    {
        public CreatePrintWeekTemplateCommand(int id
            , int daysPerPage
            , byte[] file
            , string fileName) : base(id)
        {
            DaysPerPage = daysPerPage;
            PrintFile = file;
            PrintFileName = fileName;

        }
        public int DaysPerPage { get; set; }

        /// <summary>
        /// Word-документ с определенными в нем полями для отображения дня Выходной формы
        /// </summary>
        public byte[] PrintFile { get; set; }

        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        public string PrintFileName { get; set; }
    }
}
