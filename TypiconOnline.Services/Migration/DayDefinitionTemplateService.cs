using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;

namespace TypiconOnline.AppServices.Migration
{
    /// <summary>
    /// Служба для поиска шаблонных богослужебных текстов по знаку службы
    /// </summary>
    public class DayDefinitionTemplateService : IXmlMigrationService
    {
        public DayDefinitionTemplateService()
        {

        }

        public string Read(string key)
        {
            throw new NotImplementedException();
        }
    }
}
