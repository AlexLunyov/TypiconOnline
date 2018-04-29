using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemTextUnit
    {
        [XmlAttribute("language")]
        public string Language { get; set; }
        [XmlText()]
        public string Text { get; set; }
    }
}
