using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Выходная модель для параграфа с определением стиля
    /// </summary>
    [Serializable]
    [XmlRoot(ViewModelConstants.ParagraphNodeName)]
    public class ParagraphViewModel //: List<string>
    {
        [XmlAttribute( ViewModelConstants.ParagraphTextNodeName)]
        public string Language { get; set; }
        [XmlElement(ViewModelConstants.ParagraphTextNodeName)]
        public string Text { get; set; }
        [XmlElement(ViewModelConstants.ParagraphStyleNodeName)]
        public TextStyle Style { get; set; }
        [XmlElement(ViewModelConstants.ParagraphNoteNodeName)]
        public ParagraphViewModel Note { get; set; }

        public ParagraphViewModel() { }

        //public ParagraphViewModel(ItemTextNoted itemTextNoted, string language)
        //{
        //    Style = itemTextNoted.Style;

        //    if (itemTextNoted[language] is string s)
        //    {
        //        Add(s);
        //    }

        //    if (itemTextNoted.Note != null)
        //    {
        //        Note = new ParagraphViewModel(itemTextNoted.Note, language);
        //    }
        //}

        /// <summary>
        /// Заменяет указанное строковое значение на новое 
        /// </summary>
        /// <param name="oldValue">Старое значение</param>
        /// <param name="newValue">Новое значение</param>
        public void Replace(string oldValue, string newValue)
        {
            Text = Text.Replace(oldValue, newValue);
        }
    }
}
