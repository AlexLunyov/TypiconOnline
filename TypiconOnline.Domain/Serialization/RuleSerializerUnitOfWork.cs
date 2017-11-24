using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Serialization
{
    public class RuleSerializerUnitOfWork : IRuleSerializerUnitOfWork
    {
        protected Dictionary<string, object> factories = new Dictionary<string, object>();

        /// <summary>
        /// Контейнер фабрик для элемента Правила или его наследников
        /// </summary>
        /// <typeparam name="T">Елемент правила. Также учитываются все его наследники</typeparam>
        /// <returns></returns>
        public RuleBaseSerializerContainer<T> Factory<T>() where T : RuleElement
        {
            string key = typeof(T).Name;
            if (factories.Keys.Contains(key) == true)
            {
                return factories[key] as RuleBaseSerializerContainer<T>;
            }
            RuleBaseSerializerContainer<T> factory = new RuleXmlSerializerContainer<T>(this);
            factories.Add(key, factory);
            return factory;        
        }
        /// <summary>
        /// Контейнер фабрик для элемента Правила или его наследников
        /// </summary>
        /// <typeparam name="T">Елемент правила. Также учитываются все его наследники</typeparam>
        /// <typeparam name="U">Второе условие</typeparam>
        /// <returns></returns>
        public RuleBaseSerializerContainer<T> Factory<T, U>() where T : RuleElement
        {
            string key = typeof(T).Name + typeof(U).Name;

            if (factories.Keys.Contains(key) == true)
            {
                return factories[key] as RuleBaseSerializerContainer<T>;
            }
            RuleBaseSerializerContainer<T> factory = new RuleXmlSerializerContainer<T, U>(this);
            factories.Add(key, factory);
            return factory;
        }
    }
}
