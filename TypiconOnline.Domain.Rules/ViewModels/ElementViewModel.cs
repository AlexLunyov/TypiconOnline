using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Коллекция выходных форм правил, которые будут выводиться наружу,
    /// т.е. будут являться конечным результатом обработки правил
    /// </summary>
    [CollectionDataContract]
    public class ElementViewModel : List<ViewModelItem>
    {
    }

}
