using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                result = _fileReader.Read(string.Format("[template].{0}", dayOfWeek));
            }
            
            return result;
        }

  }
}
