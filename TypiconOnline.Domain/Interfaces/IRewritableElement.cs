using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс для элементов правил последовательностей, которые могут быть заменены в Добавлениях (AsAddition)
    /// </summary>
    public interface IRewritableElement
    {
        /// <summary>
        /// Прямой родитель
        /// </summary>
        IRewritableElement Parent { get; }

        /// <summary>
        /// Уникальное имя элемента, по которому будет вестись поиск для замены.
        /// Включает всю иерархию элементов
        /// </summary>
        /// <example>
        /// 
        /// Пример для замены KAfterRule для 3-ей песни канона
        /// 
        /// service?id=1/kanonasrule/k_after?number=3
        /// 
        /// </example>
        string RewritableName { get; }

        /// <summary>
        /// Признак того, что данный элемент переписывает функционал шаблона
        /// </summary>
        bool Rewrite { get; set; }

        /// <summary>
        /// Метод сравнения для замены элемента
        /// </summary>
        /// <param name="element">Элемент для сравнения</param>
        /// <returns></returns>
        //bool Equals(IRewritableElement element);
    }
}
