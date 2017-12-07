using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        [XmlArray(RuleConstants.BookReadingAnnotationNode)]
        [XmlArrayItem(ElementName = RuleConstants.BookReadingStihosNode, Type = typeof(BookStihos))]
        public virtual List<BookStihos> Annotation { get; set; } = new List<BookStihos>();

        /// <summary>
        /// Сам текст
        /// </summary>
        [XmlArray(RuleConstants.BookReadingTextNode)]
        [XmlArrayItem(ElementName = RuleConstants.BookReadingStihosNode, Type = typeof(BookStihos))]
        public virtual List<BookStihos> Text { get; set; } = new List<BookStihos>();

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
