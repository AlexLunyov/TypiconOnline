using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Абстрактный контейнер для сериализаторов для создания элементов правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuleSerializerContainerBase<T> where T: IRuleElement
    {
        protected Dictionary<string, IRuleSerializer> _factories = new Dictionary<string, IRuleSerializer>();

        protected IRuleSerializerRoot _serializerRoot;

        //TODO: используется только для тестов. В дальнейшем удалить
        protected IDescriptor _descriptor;

        public RuleSerializerContainerBase(IRuleSerializerRoot serializerRoot, IDescriptor descriptor)
        {
            _serializerRoot = serializerRoot ?? throw new ArgumentNullException("unitOfWork");
            _descriptor = descriptor ?? throw new ArgumentNullException("descriptor");

            LoadFactories();
        }

        protected abstract void LoadFactories();

        //TODO: используется только для тестов. В дальнейшем удалить
        public T Deserialize(string description)
        {
            return Deserialize(_descriptor.CreateInstance(description), null);
        }

        public T Deserialize(IDescriptor descriptor, IAsAdditionElement parent)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor"); 

            string elementName = descriptor.GetElementName();

            if (_factories.ContainsKey(elementName))
            {
                return (T) _factories[elementName].Deserialize(descriptor, parent);
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
