using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Factories
{
    /// <summary>
    /// Реализация контейнера фабрик для xml-вариации представления элементов правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RuleXmlFactoryContainer<T> : RuleBaseFactoryContainer<T> where T: RuleElement
    {
        public RuleXmlFactoryContainer() : base(new XmlDescriptor()) { }

        protected override void LoadFactories()
        {
            _factories = new Dictionary<string, IRuleFactory<T>>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.IsSubclassOf(typeof(RuleXmlFactoryBase)) 
                    && type.GetInterface(typeof(IRuleFactory<T>).ToString()) != null)
                {
                    var factory = Assembly.GetExecutingAssembly().CreateInstance(type.AssemblyQualifiedName) as IRuleFactory<T>;

                    if (factory?.ElementNames != null)
                    {
                        foreach (string name in factory.ElementNames)
                        {
                            _factories.Add(name, factory);
                        }
                    }
                }
            }
        }
    }
}
