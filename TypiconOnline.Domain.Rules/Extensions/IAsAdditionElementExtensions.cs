using System;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class IAsAdditionElementExtensions
    {
        /// <summary>
        /// Обрабатывает возможности rewrite/remove элемента IAsAdditionElement.
        /// Если элемент найден и обработан, возвращает true.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static bool AsAdditionHandled(this IAsAdditionElement element, IRuleHandler handler)
        {
            bool result = false;
            if (handler.Settings.Addition?.RuleContainer is ExecContainer container)
            {
                //ищем элемент(ы) для замены
                var foundItems = container.GetAsAdditionChildElements(handler.Settings.Addition, element,
                        c => (c.AsAdditionMode == AsAdditionMode.Rewrite 
                           || c.AsAdditionMode == AsAdditionMode.Remove
                           || c.AsAdditionMode == AsAdditionMode.RewriteValues));

                //если находим, исполняем/исключаем его вместо настоящего элемента
                foreach (var found in foundItems)
                {
                    if (found != null)
                    {
                        switch (found.AsAdditionMode)
                        {
                            case AsAdditionMode.Rewrite:
                                {
                                    //если rewrite, то исполняем элемент
                                    Rewrite(found, handler);

                                    result = true;
                                }
                                break;
                            case AsAdditionMode.Remove:
                                {
                                    result = true;
                                    //если remove, то просто ничего не делаем
                                }
                                break;
                            case AsAdditionMode.RewriteValues:
                                {
                                    //переписываем внутренние значения элемента и только
                                    element.RewriteValues(found);
                                }
                                break;
                        }
                    }
                }
            }

            return result;
        }

        private static void Rewrite(IAsAdditionElement found, IRuleHandler handler)
        {
            var currentsettings = handler.Settings;

            handler.Settings = currentsettings.Addition;
            (found as RuleElementBase).Interpret(handler);

            handler.Settings = currentsettings;
        }

        /// <summary>
        /// Возвращает уровень глубины элемента <see cref="IAsAdditionElement"/>
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static int GetDepth(this IAsAdditionElement elem)
        {
            int result = 0;

            while (elem.Parent != null)
            {
                elem = elem.Parent;
                result++;
            }

            return result;
        }

        /// <summary>
        /// Сравнение с элементом <see cref="IAsAdditionElement"/> на совпадение
        /// </summary>
        /// <param name="elem">Элемент в основном правиле</param>
        /// <param name="elemToMatch">Элемент в Дополнении</param>
        /// <returns></returns>
        public static AsAdditionMatchingResult IsMatch(this IAsAdditionElement elem, IAsAdditionElement elemToMatch)
        {
            //Находим разницу в уровнях глубины элементов
            int dif = elem.GetDepth() - elemToMatch.GetDepth();

            /* 
             * Зашли слишком глубоко - надо останавливаться, сравнение имеет отрицательный результат
             * Например, элемент в основном правиле - worship?id=moleben
             * А элемент в дополнении - worship/kekragaria
             */
            if (dif < 0)
            {
                return AsAdditionMatchingResult.Fail;
            }

            /* 
             * Уровни одинаковые - значит сравниваем напрямую
             */
            if (dif == 0)
            {
                return (elem.AsAdditionName == elemToMatch.AsAdditionName) 
                    ? AsAdditionMatchingResult.Success 
                    : AsAdditionMatchingResult.Fail;
            }

            //Элемент в дополнении имеет меньшую глубину
            
            var compareResult = AsAdditionMatchingResult.Continue;

            //сравниваем родителей, пока не выйдем на одинаковый уровень
            while (compareResult == AsAdditionMatchingResult.Continue)
            {
                elem = elem.Parent;
                compareResult = elem.IsMatch(elemToMatch);
            }

            //Если на одинаковом уровне значения совпадают, значит возвращаем Continue
            //Иначе  - Fail
            return (compareResult == AsAdditionMatchingResult.Success) 
                ? AsAdditionMatchingResult.Continue 
                : AsAdditionMatchingResult.Fail;
        }
    }
}
