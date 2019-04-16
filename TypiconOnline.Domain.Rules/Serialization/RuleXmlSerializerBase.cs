using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    public abstract class RuleXmlSerializerBase : IRuleSerializer, IRuleSerializer<IRuleElement>
    {
        protected RuleXmlSerializerBase(IRuleSerializerRoot serializerRoot)
        {
            SerializerRoot = serializerRoot ?? throw new ArgumentNullException(nameof(serializerRoot));
        }

        protected IRuleSerializerRoot SerializerRoot { get; }

        public IEnumerable<string> ElementNames { get; protected set; }

        public virtual IRuleElement Deserialize(IDescriptor descriptor, IAsAdditionElement parent)
        {
            IRuleElement element = null;

            if (descriptor is XmlDescriptor d)
            {
                element = CreateObject(new CreateObjectRequest() { Descriptor = d, Parent = parent });

                /*
                 * Вычисляем IRewritableElement.
                 * Если созданный элемент является таковым, используем его.
                 * Если нет - используем parent
                 */

                parent = element as IAsAdditionElement ?? parent;

                FillObject(new FillObjectRequest() { Descriptor = d, Element = element, Parent = parent });
            }

            return element;
        }

        protected abstract IRuleElement CreateObject(CreateObjectRequest req);
        protected abstract void FillObject(FillObjectRequest req);

        public abstract string Serialize(IRuleElement element);
    }
}
