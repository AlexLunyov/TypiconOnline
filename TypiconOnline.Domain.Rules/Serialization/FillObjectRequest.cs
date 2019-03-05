using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Запрос для заполнения десериализуемого объекта
    /// </summary>
    public class FillObjectRequest
    {
        /// <summary>
        /// Xml-описание
        /// </summary>
        public XmlDescriptor Descriptor { get; set; }
        /// <summary>
        /// Элемент для заполнения
        /// </summary>
        public IRuleElement Element { get; set; }
        /// <summary>
        /// Вышестоящий по иерархии заменяемый элемент
        /// </summary>
        public IAsAdditionElement Parent { get; set; }
    }
}
