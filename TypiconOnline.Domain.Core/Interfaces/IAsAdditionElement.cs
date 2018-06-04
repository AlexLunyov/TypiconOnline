using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для элементов правил последовательностей, которые могут быть заменены в Добавлениях (AsAddition)
    /// </summary>
    public interface IAsAdditionElement
    {
        /// <summary>
        /// Прямой родитель
        /// </summary>
        IAsAdditionElement Parent { get; }

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
        string AsAdditionName { get; }

        /// <summary>
        /// Варианты поведения элементов, помеченных как Дополнения (AsAddition)
        /// </summary>
        AsAdditionMode AsAdditionMode { get; set; }

        /// <summary>
        /// Метод присваивает свойствам Элемента значения введенного source.
        /// </summary>
        /// <param name="source">Элемент со значениями для замены.</param>
        void RewriteValues(IAsAdditionElement source);
    }

    /// <summary>
    /// Варианты поведения элементов, помеченных как Дополнения (AsAddition)
    /// </summary>
    /// <see cref="TypiconOnline.Domain.Interfaces.IAsAdditionElement"/>
    public enum AsAdditionMode
    {
        /// <summary>
        /// Никаких действий не будет производиться. Значение по умолчанию
        /// </summary>
        None,
        /// <summary>
        /// Элемент будет перезаписан
        /// </summary>
        Rewrite,
        /// <summary>
        /// Элемент будет добавлен
        /// </summary>
        Append,
        /// <summary>
        /// Элемент будет исключен из обработки Правила
        /// </summary>
        Remove,
        /// <summary>
        /// Будут переписаны только свойства обновляемого элемента
        /// </summary>
        RewriteValues
    }
}
