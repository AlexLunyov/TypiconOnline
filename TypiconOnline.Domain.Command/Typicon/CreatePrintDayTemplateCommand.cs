using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class CreatePrintDayTemplateCommand : CreateRuleCommandBase<PrintDayTemplate>
    {
        public CreatePrintDayTemplateCommand(int id
            , int number
            , string name
            , int? icon
            , bool isRed
            , byte[] file
            , string fileName
            , bool isDefault) : base(id)
        {
            Number = number;
            Name = name;
            Icon = icon;
            IsRed = isRed;
            PrintFile = file;
            PrintFileName = fileName;
            IsDefault = isDefault;
        }
        public int Number { get; set; }

        /// <summary>
        /// Наименование. Используется исключительно для информативности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Символ отображения знака службы. Используется в веб-версии расписания
        /// </summary>
        public int? Icon { get; set; }

        public bool IsRed { get; set; }
        /// <summary>
        /// Word-документ с определенными в нем полями для отображения дня Выходной формы
        /// </summary>
        public byte[] PrintFile { get; set; }

        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        public string PrintFileName { get; set; }
        /// <summary>
        /// Признак Шаблона по умолчанию
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
