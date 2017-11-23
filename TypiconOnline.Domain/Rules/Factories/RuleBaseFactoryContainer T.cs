using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Rules.Factories
{
    /// <summary>
    /// Абстрактная фабрика для создания элементов правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuleBaseFactoryContainer<T> where T: RuleElement
    {
        protected Dictionary<string, IRuleFactory<T>> _factories;

        protected IDescriptor _descriptor;

        public RuleBaseFactoryContainer(IDescriptor descriptor)
        {
            _descriptor = descriptor ?? throw new ArgumentNullException("descriptor");

            LoadFactories();
        }

        protected abstract void LoadFactories();

        //public T CreateElement(string description)
        //{
        //    _descriptor.Description = description;

        //    string elementName = _descriptor.GetElementName();

        //    if (_factories.ContainsKey(elementName))
        //    {
        //        return _factories[elementName].Create(_descriptor);
        //    }

        //    return null;
        //}

        public T CreateElement(IDescriptor descriptor)
        {
            if (string.IsNullOrEmpty(descriptor?.GetElementName())) throw new ArgumentException("descriptor"); 

            string elementName = descriptor.GetElementName();

            if (_factories.ContainsKey(elementName))
            {
                return _factories[elementName].Create(descriptor);
            }

            return null;
        }
    }
}
