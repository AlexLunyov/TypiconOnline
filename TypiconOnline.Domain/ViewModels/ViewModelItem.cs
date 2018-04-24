using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Элементарная частичка выходной формы последовательности богослужений
    /// </summary>
    [Serializable]
    [XmlRoot(ViewModelConstants.ViewModelItemNodeName)]
    public class ViewModelItem 
    {
        [XmlAttribute(ViewModelConstants.ViewModelKindTextAttrName)]
        public string KindStringValue { get; set; }

        [XmlAttribute(ViewModelConstants.ViewModelKindAttrName)]
        public ViewModelItemKind Kind { get; set; }

        [XmlArray(ElementName = ViewModelConstants.ViewModelItemChildNodeName, IsNullable = true)]
        [XmlArrayItem(ElementName = ViewModelConstants.ParagraphNodeName, Type = typeof(ParagraphViewModel))]
        //TODO: необходимо добавить к строковым значениям каждого параграфа также и стиль
        public List<ParagraphViewModel> Paragraphs { get; set; }
    }

    public enum ViewModelItemKind { Choir, Lector, Priest, Deacon, Stihos, Text, Irmos, Troparion, Chorus, Theotokion }
}
