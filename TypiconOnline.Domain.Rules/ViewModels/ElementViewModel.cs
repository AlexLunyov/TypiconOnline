using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Элементарная частичка выходной формы последовательности богослужений
    /// </summary>
    [Serializable]
    [XmlRoot(ViewModelConstants.ElementViewModelNodeName)]
    public class ElementViewModel 
    {
        [XmlAttribute(ViewModelConstants.ViewModelKindTextAttrName)]
        public string KindValue { get; set; }

        [XmlAttribute(ViewModelConstants.ViewModelKindAttrName)]
        public ElementViewModelKind Kind { get; set; }

        [XmlArray(ElementName = ViewModelConstants.ElementViewModelChildNodeName, IsNullable = true)]
        [XmlArrayItem(ElementName = ViewModelConstants.ParagraphNodeName, Type = typeof(ParagraphViewModel))]
        //TODO: необходимо добавить к строковым значениям каждого параграфа также и стиль
        public List<ParagraphViewModel> Paragraphs { get; set; }
    }

    public enum ElementViewModelKind { Choir, Lector, Priest, Deacon, Stihos, Text, Irmos, Troparion, Chorus, Theotokion }
}
