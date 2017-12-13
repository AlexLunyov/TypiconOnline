using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Schedule;

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
