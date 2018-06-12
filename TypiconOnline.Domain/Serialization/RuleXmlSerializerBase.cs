using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Serialization
{
    public abstract class RuleXmlSerializerBase : IRuleSerializer
    {
        public RuleXmlSerializerBase(IRuleSerializerRoot root)
        {
            SerializerRoot = root ?? throw new ArgumentNullException("RuleSerializerRoot");
        }

        protected IRuleSerializerRoot SerializerRoot { get; }

        public IEnumerable<string> ElementNames { get; protected set; }

        public virtual IRuleElement Deserialize(IDescriptor descriptor, IAsAdditionElement parent)
        {
            RuleElement element = null;

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

        protected abstract RuleElement CreateObject(CreateObjectRequest req);
        protected abstract void FillObject(FillObjectRequest req);

        public abstract string Serialize(IRuleElement element);
    }
}
