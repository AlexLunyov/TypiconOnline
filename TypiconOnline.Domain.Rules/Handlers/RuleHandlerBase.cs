using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Базовый класс для обработчиков правил Rules
    /// </summary>
    public abstract class RuleHandlerBase : IRuleHandler
    {
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
            }
        }

        public virtual bool IsTypeAuthorized(ICustomInterpreted t) 
        {
            return IsTypeAuthorized(t.GetType());
        }

        //public virtual bool IsAuthorized<T>() where T : ICustomInterpreted
        //{
        //    return IsTypeAuthorized(typeof(T));
        //}

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
        public abstract bool Execute(ICustomInterpreted element);

        public abstract void ClearResult();
    }
}
