using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Коллекция выходных форм правил, которые будут выводиться наружу,
    /// т.е. будут являться конечным результатом обработки правил
    /// </summary>
    [CollectionDataContract]
    public class OutputElementCollection : List<OutputSection>
    {
    }

}
