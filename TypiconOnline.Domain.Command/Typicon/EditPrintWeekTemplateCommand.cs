using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.Domain.Command.Typicon
{
    public class EditPrintWeekTemplateCommand : EditRuleCommandBase<PrintWeekTemplate>
    {
        public EditPrintWeekTemplateCommand(int id
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
