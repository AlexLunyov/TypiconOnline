using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Handlers
{

    /// <summary>
    /// Обработчик правил собирает в коллекцию элементы определенного типа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectorRuleHandler<T> : RuleHandlerBase //where T : RuleElement, ICustomInterpreted
    {
        protected ExecContainer _executingResult;

        public CollectorRuleHandler()
        {
            AuthorizedTypes = new List<Type>()
            {
                typeof(T),
            };

            _executingResult = null;
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
            if (element is T && element is RuleElement r)
            {
                if (_executingResult == null)
                {
                    _executingResult = new ExecContainer();
                }

                _executingResult.ChildElements.Add(r);

                return true;
            }
            return false;
        }

        public ExecContainer GetResult()
        {
            return _executingResult;
        }
    }
}
