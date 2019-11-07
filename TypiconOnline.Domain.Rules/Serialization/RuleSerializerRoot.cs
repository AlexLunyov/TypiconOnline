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

        public RuleSerializerRoot([NotNull] IQueryProcessor queryProcessor, ITypiconSerializer typiconSerializer)
        {
            QueryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            TypiconSerializer = typiconSerializer ?? throw new ArgumentNullException(nameof(typiconSerializer));
        }

        public IQueryProcessor QueryProcessor { get; }
        public ITypiconSerializer TypiconSerializer { get; }

        /// <summary>
        /// Контейнер фабрик для элемента Правила или его наследников
        /// </summary>
        /// <typeparam name="T">Елемент правила. Также учитываются все его наследники</typeparam>
        /// <returns></returns>
        public virtual IRuleSerializerContainer<T> Container<T>() where T : IRuleElement
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
        /// <typeparam name="T1">Елемент правила. Также учитываются все его наследники</typeparam>
        /// <typeparam name="T2">Второе условие</typeparam>
        /// <returns></returns>
        public virtual IRuleSerializerContainer<T1> Container<T1, T2>() where T1 : IRuleElement
        {
            string key = typeof(T1).Name + typeof(T2).Name;

            if (factories.Keys.Contains(key) == true)
            {
                return factories[key] as RuleSerializerContainerBase<T1>;
            }
            RuleSerializerContainerBase<T1> factory = new RuleXmlSerializerContainer<T1, T2>(this);
            factories.Add(key, factory);
            return factory;
        }
    }
}
