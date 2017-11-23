using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Factories
{
    public class RuleFactoryUnitOfWork : IRuleFactoryUnitOfWork
    {
        protected Dictionary<Type, object> factories = new Dictionary<Type, object>();

        public RuleBaseFactoryContainer<T> Factory<T>() where T : RuleElement
        {
            if (factories.Keys.Contains(typeof(T)) == true)
            {
                return factories[typeof(T)] as RuleBaseFactoryContainer<T>;
            }
            RuleBaseFactoryContainer<T> factory = new RuleXmlFactoryContainer<T>();
            factories.Add(typeof(T), factory);
            return factory;        
        }
    }
}
