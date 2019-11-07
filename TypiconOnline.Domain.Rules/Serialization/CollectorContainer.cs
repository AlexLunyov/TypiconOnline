using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Serialization
{
    public class CollectorContainer<T> : IRuleSerializerContainer<T> where T : IRuleElement
    {
        private readonly IRuleSerializerContainer<T> _innerContainer;
        private readonly CollectorSerializerRoot _serializerRoot;

        public CollectorContainer(IRuleSerializerContainer<T> container, CollectorSerializerRoot serializerRoot)
        {
            _innerContainer = container ?? throw new ArgumentNullException(nameof(container));
            _serializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }

        public T Deserialize(string description)
        {
            var result = _innerContainer.Deserialize(description);

            if (result is IHavingVariables c)
            {
                _serializerRoot.AddHavingVariablesElement(c);
            }

            return result;
        }

        public T Deserialize(IDescriptor descriptor, IAsAdditionElement parent)
        {
            var result = _innerContainer.Deserialize(descriptor, parent);

            if (result is IHavingVariables c)
            {
                _serializerRoot.AddHavingVariablesElement(c);
            }

            return result;
        }

        public string Serialize(T element)
        {
            throw new NotImplementedException();
        }
    }
}
