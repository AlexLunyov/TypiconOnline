using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule.Extensions
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
            if (handler.Settings.Addition != null)
            {
                //ищем элемент для замены
                var found = handler.Settings.Addition.RuleContainer
                    .GetChildElements<IAsAdditionElement>(handler.Settings.Addition,
                        c => c.AsAdditionName == element.AsAdditionName 
                            && (c.AsAdditionMode == AsAdditionMode.Rewrite || c.AsAdditionMode == AsAdditionMode.Remove)).FirstOrDefault();

                //если находим, исполняем/исключаем его вместо настоящего элемента
                if (found != null)
                {
                    //если rewrite, то исполняем элемент
                    //если remove, то просто ничего не делаем
                    if (found.AsAdditionMode == AsAdditionMode.Rewrite)
                    {
                        var currentsettings = handler.Settings;

                        handler.Settings = currentsettings.Addition;
                        (found as RuleElement).Interpret(handler);

                        handler.Settings = currentsettings;
                    }

                    result = true;
                }
            }

            return result;
        }
    }
}
