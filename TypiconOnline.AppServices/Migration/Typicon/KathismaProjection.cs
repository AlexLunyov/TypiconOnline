using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class KathismaProjection
    {
        public KathismaProjection() { }
        public int Number { get; set; }
        public ItemText NumberName { get; set; }
        [XmlArray("SlavaElements")]
        [XmlArrayItem("SlavaElement")]
        public List<SlavaElementProjection> SlavaElements { get; set; } = new List<SlavaElementProjection>();
    }
}