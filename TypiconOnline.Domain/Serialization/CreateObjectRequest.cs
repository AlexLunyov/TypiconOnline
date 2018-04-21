using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Запрос для создания десериализуемого объекта
    /// </summary>
    public class CreateObjectRequest
    {
        /// <summary>
        /// Xml-описание
        /// </summary>
        public XmlDescriptor Descriptor { get; set; }
        /// <summary>
        /// Вышестоящий по иерархии заменяемый элемент
        /// </summary>
        public IAsAdditionElement Parent { get; set; }
    }
}
