using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

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
        public RuleElement Element { get; set; }
        /// <summary>
        /// Вышестоящий по иерархии заменяемый элемент
        /// </summary>
        public IAsAdditionElement Parent { get; set; }
    }
}
