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
        /// Пока работать будет только этот признак.
        /// </summary>
        public bool LoadRelatedEntities { get; set; }
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
            string result = LoadRelatedEntities ? "" : "nlre";

            return result;
        }
    }
}
