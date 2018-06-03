using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    /// <summary>
    /// Предназначен для описания настроек для загрузки связанных данных в репозитории
    /// </summary>
    public class IncludeOptions
    {
        /// <summary>
        /// Список свойств, которые необходимо загрузить.
        /// Если необходимо загрузить вложенные свойства объектов, пишем через "."
        /// </summary>
        public string[] Includes { get; set; }
        /// <summary>
        /// Признак, загружать ли единичные связанные объекты
        /// </summary>
        //public bool IncludeSingleEntities { get; set; }
        /// <summary>
        /// Признак, загружать ли связанные коллекции
        /// </summary>
        //public bool IncludeCollections { get; set; }

        public string GetKey()
        {
            string result = (Includes != null) ? "" : "nlre";

            return result;
        }
    }
}
