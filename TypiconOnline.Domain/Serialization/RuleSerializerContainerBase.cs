using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Абстрактный контейнер для сериализаторов для создания элементов правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuleSerializerContainerBase<T> where T: RuleElement
    {
        protected Dictionary<string, IRuleSerializer> _factories = new Dictionary<string, IRuleSerializer>();

        protected IRuleSerializerRoot _unitOfWork;

        protected IDescriptor _descriptor;

        public RuleSerializerContainerBase(IRuleSerializerRoot unitOfWork, IDescriptor descriptor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _descriptor = descriptor ?? throw new ArgumentNullException("descriptor");

            LoadFactories();
        }

        protected abstract void LoadFactories();

        public T Deserialize(string description)
        {
            return Deserialize(_descriptor.CreateInstance(description));
        }

        public T Deserialize(IDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor"); 

            string elementName = descriptor.GetElementName();

            if (_factories.ContainsKey(elementName))
            {
                return _factories[elementName].Deserialize(descriptor) as T;
            }

            return default(T);
        }

        public string Serialize(T element)
        {
            string result = "";

            if (_factories.ContainsKey(element.ElementName))
            {
                result = _factories[element.ElementName].Serialize(element);
            }

            return result;
        }

        public override string ToString()
        {
            int count = (_factories != null) ? _factories.Count : 0;
            return $"{GetType().Name}; Factories count: {count}";
        }
    }
}
