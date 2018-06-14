using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{

    /// <summary>
    /// Обработчик правил собирает в коллекцию элементы определенного типа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectorRuleHandler<T> : RuleHandlerBase //where T : RuleElement, ICustomInterpreted
    {
        protected List<T> executingResult = new List<T>();

        public CollectorRuleHandler()
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(T)
            };

            ClearResult();
        }

        public override bool IsAuthorized<U>()
        {
            bool result = base.IsAuthorized<U>();

            foreach (var type in AuthorizedTypes)
            {
                result ^= type.IsInterface && typeof(U).GetInterface(type.Name) != null;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element">Существует ограничение на то, чтобы элемент был наследником класса RuleElement</param>
        /// <returns></returns>
        public override bool Execute(ICustomInterpreted element)
        {
            if (element is RuleElement && element is T r)
            {
                executingResult.Add(r);

                return true;
            }
            return false;
        }

        public IReadOnlyList<T> GetResult() => executingResult;

        public override void ClearResult()
        {
            //executingResult = new List<T>();
            executingResult.Clear();
        }
    }
}
