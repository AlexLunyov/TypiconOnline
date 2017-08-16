using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rendering;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Базовый класс для обработчиков правил Rules
    /// </summary>
    public abstract class RuleHandlerBase : IRuleHandler
    {
        protected List<Type> AuthorizedTypes;
        //protected RenderContainer _executingResult;
        protected RuleHandlerSettings _settings = new RuleHandlerSettings();

        public RuleHandlerBase() { }

        public virtual RuleHandlerSettings Settings
        {
            get
            {
                return _settings;
            }

            set
            {
                _settings = value;
                //_executingResult = null;
            }
        }

        public bool IsTypeAuthorized(ICustomInterpreted t) 
        {
            return AuthorizedTypes.Contains(t.GetType());
        }

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            return AuthorizedTypes.Contains(typeof(T));
        }

        /// <summary>
        /// Абстрактный метод обработки правила
        /// </summary>
        /// <param name="element"></param>
        public abstract void Execute(ICustomInterpreted element);

        //public abstract RuleContainer GetResult();
        //public virtual RenderContainer GetResult()
        //{
        //    return _executingResult;
        //}

        //public abstract void Initialize(RuleHandlerSettings request);
    }
}
