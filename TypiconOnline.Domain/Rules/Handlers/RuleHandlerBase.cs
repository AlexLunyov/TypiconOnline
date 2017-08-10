using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Handlers
{
    public abstract class RuleHandlerBase : IRuleHandler
    {
        protected List<Type> AuthorizedTypes;
        protected RuleContainer _executingResult;
        protected RuleHandlerSettings _settings = new RuleHandlerSettings();

        public RuleHandlerBase() { }

        public RuleHandlerSettings Settings
        {
            get
            {
                return _settings;
            }

            set
            {
                _settings = value;
                _executingResult = null;
            }
        }

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            return AuthorizedTypes.Contains(typeof(T));
        }

        public abstract void Execute(ICustomInterpreted element);
        public abstract RuleContainer GetResult();
        //public abstract void Initialize(RuleHandlerSettings request);
    }
}
