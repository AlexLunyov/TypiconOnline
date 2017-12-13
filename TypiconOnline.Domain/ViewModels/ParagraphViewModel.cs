using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Выходная модель для параграфа с определением стиля
    /// </summary>
    public class ParagraphViewModel //: List<string>
    {
        public string Text { get; set; }
        public TextStyle Style { get; set; }
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
