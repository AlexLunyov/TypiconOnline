using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleSerializerContainer<T> where T : IRuleElement
    {
        T Deserialize(string description);
        T Deserialize(IDescriptor descriptor, IAsAdditionElement parent);
        string Serialize(T element);
    }
}
