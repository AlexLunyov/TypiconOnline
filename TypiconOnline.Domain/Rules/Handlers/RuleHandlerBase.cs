using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Базовый класс для обработчиков правил Rules
    /// </summary>
    public abstract class RuleHandlerBase : IRuleHandler
    {
        //protected RenderContainer _executingResult;
        protected RuleHandlerSettings _settings = new RuleHandlerSettings();

        public RuleHandlerBase() { }

        protected List<Type> AuthorizedTypes { get; set; }
        protected List<Type> ResctrictedTypes { get; set; }

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
            return IsTypeAuthorized(t.GetType());
        }

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            return IsTypeAuthorized(typeof(T));
        }

        private bool IsTypeAuthorized(Type type)
        {
            bool isAuthorized = false;
            if (AuthorizedTypes != null)
            {
                isAuthorized = AuthorizedTypes.Contains(type);
            }
            else if (ResctrictedTypes != null)
            {
                isAuthorized = !ResctrictedTypes.Contains(type);
            }
            return isAuthorized;
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
