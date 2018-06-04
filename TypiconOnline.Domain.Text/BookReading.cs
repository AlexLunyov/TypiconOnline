using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.Text.Common;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Чтение Священного Писания
    /// </summary>
    [Serializable]
    public class BookReading : DayElementBase
    {
        /// <summary>
        /// Коллекция стихов, свялающихся аннотацией к чтению
        /// </summary>
        [XmlArray(XmlConstants.BookReadingAnnotationNode)]
        [XmlArrayItem(ElementName = XmlConstants.BookReadingStihosNode, Type = typeof(BookStihos))]
        public virtual List<BookStihos> Annotation { get; set; } = new List<BookStihos>();

        /// <summary>
        /// Сам текст
        /// </summary>
        [XmlArray(XmlConstants.BookReadingTextNode)]
        [XmlArrayItem(ElementName = XmlConstants.BookReadingStihosNode, Type = typeof(BookStihos))]
        public virtual List<BookStihos> Text { get; set; } = new List<BookStihos>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
