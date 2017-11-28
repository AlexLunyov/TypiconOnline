using ScheduleHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Common;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconMigrationTool
{
    /// <summary>
    /// Фабрика создает экземпляры класса DayService на основе данных из БД Access, а также файловых xml-документов
    /// </summary>
    public class MigrationDayWorshipFactory : IDayWorshipFactory
    {
        private ScheduleDBDataSet.MineinikRow _row;
        private string _folderPath;

        private FileReader _fileReader;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath">Путь к общей папке с документами</param>
        public MigrationDayWorshipFactory(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath)) throw new ArgumentNullException("folderPath");

            _folderPath = folderPath;
            _fileReader = new FileReader(_folderPath);
        }

        public void Initialize(ScheduleDBDataSet.MineinikRow mineinikRow)
        {
            if (mineinikRow == null) throw new ArgumentNullException("ScheduleDBDataSet.MineinikRow");

            _row = mineinikRow;
        }

        public DayWorship Create()
        {
            if (_row == null) throw new ArgumentNullException("ScheduleDBDataSet.MineinikRow");

            DayWorship dayService = new DayWorship();

            //наполняем содержимое текста службы
            dayService.WorshipName.AddElement("cs-ru", _row.Name);
            dayService.IsCelebrating = !_row.IsIsCelebratingNull() ? _row.IsCelebrating : false;
            dayService.UseFullName = !_row.IsUseFullNameNull() ? _row.UseFullName : false;

            if (!_row.IsShortNameNull() && !string.IsNullOrEmpty(_row.ShortName))
            {
                //dayService.ServiceShortName = new ItemText();
                dayService.WorshipShortName.AddElement("cs-ru", _row.ShortName);
            }

            string fileName = (!_row.IsDateBNull()) ? new ItemDate(_row.DateB.Month, _row.DateB.Day).Expression + "." + _row.Name : _row.Name;

            //сначала ищем в папке Menology в надежде, что текст определен (как в последствии и должно быть)
            _fileReader.FolderPath = _folderPath + "Menology\\";

            string definition = _fileReader.Read(fileName);

            if (string.IsNullOrEmpty(definition))
            {
                //Если его мы не находим, то заменяем текстом по умолчанию, исходя из знака службы
                _fileReader.FolderPath = _folderPath + "Templates\\";
                fileName = SignMigrator.Instance(_row.SignID).MajorTemplateName;
                definition = TransformDefinition(_fileReader.Read(fileName), _row.Name, fileName);
            }

            dayService.DayDefinition = definition;

            return dayService;
        }

        /// <summary>
        /// Модифицирует строку, изменяя item - на название службы
        /// sign - название знака службы
        /// </summary>
        /// <param name="xmlDefinition">xml</param>
        /// <param name="item">название службы</param>
        /// <param name="sign">название знака службы</param>
        /// <returns></returns>
        private string TransformDefinition(string xmlDefinition, string item, string sign)
        {
            return xmlDefinition.Replace("[item]", item)
                                         .Replace("[sign]", sign);
        }
    }
}
