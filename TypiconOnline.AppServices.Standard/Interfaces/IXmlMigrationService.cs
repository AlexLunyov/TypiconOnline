using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Interfaces
{
    /// <summary>
    /// Интерфейс для классов, экспортирующих xml-описания правил и текстов служб
    /// </summary>
    public interface IXmlMigrationService
    {
        string Read(string key);
    }
}
