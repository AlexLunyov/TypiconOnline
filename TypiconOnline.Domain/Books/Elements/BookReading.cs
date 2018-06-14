using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Books.Elements
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
        [XmlArray(ElementConstants.BookReadingAnnotationNode)]
        [XmlArrayItem(ElementName = ElementConstants.BookReadingStihosNode, Type = typeof(BookStihos))]
        public virtual List<BookStihos> Annotation { get; set; } = new List<BookStihos>();

        /// <summary>
        /// Сам текст
        /// </summary>
        [XmlArray(ElementConstants.BookReadingTextNode)]
        [XmlArrayItem(ElementName = ElementConstants.BookReadingStihosNode, Type = typeof(BookStihos))]
        public virtual List<BookStihos> Text { get; set; } = new List<BookStihos>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
