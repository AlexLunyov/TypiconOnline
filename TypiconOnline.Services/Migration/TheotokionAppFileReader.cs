using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.AppServices.Migration
{
    public class TheotokionAppFileReader : MigrationFileReaderBase, ITheotokionAppFileReader
    {
        public TheotokionAppFileReader(IFileReader fileReader) : base(fileReader) { }

        public string Read(PlaceYmnosSource place, int ihos, DayOfWeek dayOfWeek)
        {
            string result = _fileReader.Read(string.Format("{0}.{1}.{2}", place, ihos, dayOfWeek));

            if (string.IsNullOrEmpty(result))
            {
                //создаем фейковый объект
                result = CreateFakeXml(place, ihos, dayOfWeek);
            }

            return result;
        }

        private string CreateFakeXml(PlaceYmnosSource place, int ihos, DayOfWeek dayOfWeek)
        {
            string text = string.Format("[{0}] [глас {1}] [{2}] Богородичен из Приложения Ирмология", place, ihos, dayOfWeek);

            Ymnos ymnos = new Ymnos();
            ymnos.Text.AddElement("cs-ru", text);

            return new TypiconSerializer().Serialize(ymnos);
        }
    }
}
