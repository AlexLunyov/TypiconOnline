using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditPrintDayTemplateCommand : EditRuleCommandBase<PrintDayTemplate>
    {
        public EditPrintDayTemplateCommand(int id
            , int number
            , string name
            , int? icon
            , bool isRed
            , byte[] file
            , string fileName) : base(id)
        {
            Number = number;
            Name = name;
            Icon = icon;
            IsRed = isRed;
            PrintFile = file;
            PrintFileName = fileName;

        }
        public int Number { get; set; }
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
    }
}
