using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Serialization
{
    public class RuleSerializerRoot : IRuleSerializerRoot
    {
        protected Dictionary<string, object> factories = new Dictionary<string, object>();

        public RuleSerializerRoot([NotNull] IDataQueryProcessor queryProcessor)
        {
            QueryProcessor = queryProcessor ?? throw new ArgumentNullException("queryProcessor in RuleSerializerRoot");
        }

        public IDataQueryProcessor QueryProcessor { get; }

        /// <summary>
        /// Контейнер фабрик для элемента Правила или его наследников
        /// </summary>
        /// <typeparam name="T">Елемент правила. Также учитываются все его наследники</typeparam>
        /// <returns></returns>
        public RuleSerializerContainerBase<T> Container<T>() where T : IRuleElement
        {
            string key = typeof(T).Name;
            if (factories.Keys.Contains(key) == true)
            {
                return factories[key] as RuleSerializerContainerBase<T>;
            }
            var factory = new RuleXmlSerializerContainer<T>(this);
            factories.Add(key, factory);
            return factory;        
        }
        /// <summary>
        /// Контейнер фабрик для элемента Правила или его наследников
        /// </summary>
        /// <typeparam name="T">Елемент правила. Также учитываются все его наследники</typeparam>
        /// <typeparam name="U">Второе условие</typeparam>
        /// <returns></returns>
        public RuleSerializerContainerBase<T> Container<T, U>() where T : IRuleElement
        {
            string key = typeof(T).Name + typeof(U).Name;

            if (factories.Keys.Contains(key) == true)
            {
                return factories[key] as RuleSerializerContainerBase<T>;
            }
            RuleSerializerContainerBase<T> factory = new RuleXmlSerializerContainer<T, U>(this);
            factories.Add(key, factory);
            return factory;
        }
    }
}
