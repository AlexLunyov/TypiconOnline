using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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
