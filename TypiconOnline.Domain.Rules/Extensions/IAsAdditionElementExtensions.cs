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
                //ищем элемент для замены
                var found = container.GetChildElements<IAsAdditionElement>(handler.Settings.Addition,
                        c => c.AsAdditionName == element.AsAdditionName 
                            && (c.AsAdditionMode == AsAdditionMode.Rewrite || c.AsAdditionMode == AsAdditionMode.Remove)).FirstOrDefault();

                //если находим, исполняем/исключаем его вместо настоящего элемента
                if (found != null)
                {
                    //если rewrite, то исполняем элемент
                    
                    switch (found.AsAdditionMode)
                    {
                        case AsAdditionMode.Rewrite:
                            {
                                Rewrite(found, handler);
                            }
                            break;
                        case AsAdditionMode.Remove:
                            {
                                //если remove, то просто ничего не делаем
                            }
                            break;
                        case AsAdditionMode.RewriteValues:
                            {
                                RewriteValues(found, handler);
                            }
                            break;
                    }

                    result = true;
                }
            }

            return result;
        }

        private static void RewriteValues(IAsAdditionElement found, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        private static void Rewrite(IAsAdditionElement found, IRuleHandler handler)
        {
            var currentsettings = handler.Settings;

            handler.Settings = currentsettings.Addition;
            (found as RuleElementBase).Interpret(handler);

            handler.Settings = currentsettings;
        }
    }
}
