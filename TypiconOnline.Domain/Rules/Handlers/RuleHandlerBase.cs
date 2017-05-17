using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public abstract class RuleHandlerBase : IRuleHandler
    {
        protected List<Type> AuthorizedTypes;
        private List<TypiconRule> _rules;
        private HandlingMode _mode;

        public RuleHandlerBase() { }

        #region Properties

        public HandlingMode Mode
        {
            get
            {
                return _mode;
            }
        }

        /// <summary>
        /// Список правил для обработки, отсортированный по приоритету
        /// </summary>
        public List<TypiconRule> Rules
        {
            get
            {
                return _rules;
            }
        }

        #endregion

        public abstract void Execute(ICustomInterpreted element);

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            return AuthorizedTypes.Contains(typeof(T));
        }

        public virtual void Initialize(RuleHandlerRequest request)
        {
            _mode = request.Mode;
            _rules = request.Rules;
        }

        public abstract RuleContainer GetResult();
    }
}
