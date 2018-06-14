using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Описание блаженн. Используется в структуре Литургии
    /// </summary>
    [Serializable]
    public class Makarismi : DayElementBase
    {
        public Makarismi() { }

        /// <summary>
        /// Блаженны
        /// </summary>
        [XmlElement(ElementConstants.MakarismiOdiNode)]
        public List<MakariosOdi> Links { get; set; }

        /// <summary>
        /// Непосредственное описание стихов блаженн. Используется в Октоихе
        /// </summary>
        [XmlElement(ElementConstants.MakarismiYmnisNode)]
        public YmnosGroup Ymnis { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}