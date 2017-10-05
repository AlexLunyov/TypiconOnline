using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Handlers
{

    /// <summary>
    /// Обработчик правил собирает в коллекцию элементы определенного типа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectorRuleHandler<T> : RuleHandlerBase where T : RuleElement, ICustomInterpreted
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

        public override void Execute(ICustomInterpreted element)
        {
            if (element is T)
            {
                if (_executingResult == null)
                {
                    _executingResult = new ExecContainer();
                }

                _executingResult.ChildElements.Add(element as T);
            }
        }

        public ExecContainer GetResult()
        {
            return _executingResult;
        }
    }
}
