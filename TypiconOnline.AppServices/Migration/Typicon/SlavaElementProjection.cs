using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.AppServices.Migration.Typicon
{
    [Serializable]
    public class SlavaElementProjection
    {
        public SlavaElementProjection() { }
        [XmlArray("PsalmLinks")]
        [XmlArrayItem("PsalmLink")]
        public List<PsalmLinkProjection> PsalmLinks { get; set; } = new List<PsalmLinkProjection>();
    }
}