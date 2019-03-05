using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Абстрактный контейнер для сериализаторов для создания элементов правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RuleSerializerContainerBase<T> : IRuleSerializerContainer<T> where T: IRuleElement
    {
        protected RuleSerializerContainerBase(IRuleSerializerRoot serializerRoot, IDescriptor descriptor)
        {
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
            Descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));

            LoadFactories();
        }

        protected Dictionary<string, IRuleSerializer> Factories { get; } = new Dictionary<string, IRuleSerializer>();

        protected IRuleSerializerRoot SerializerRoot { get; }

        //TODO: используется только для тестов. В дальнейшем удалить
        protected IDescriptor Descriptor { get; }

        protected abstract void LoadFactories();

        //TODO: используется только для тестов. В дальнейшем удалить
        public T Deserialize(string description)
        {
            return Deserialize(Descriptor.CreateInstance(description), null);
        }

        public T Deserialize(IDescriptor descriptor, IAsAdditionElement parent)
        {
            if (descriptor == null) throw new ArgumentNullException(nameof(descriptor)); 

            string elementName = descriptor.GetElementName();

            if (Factories.ContainsKey(elementName))
            {
                return (T) Factories[elementName].Deserialize(descriptor, parent);
            }

            return default(T);
        }

        public string Serialize(T element)
        {
            string result = "";

            if (Factories.ContainsKey(element.ElementName))
            {
                result = Factories[element.ElementName].Serialize(element);
            }

            return result;
        }

        public override string ToString()
        {
            int count = Factories?.Count ?? 0;
            return $"{GetType().Name}; Factories count: {count}";
        }
    }
}
