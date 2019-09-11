using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TypiconOnline.Domain.Rules.Output
{
    /// <summary>
    /// Коллекция выходных форм правил, которые будут выводиться наружу,
    /// т.е. будут являться конечным результатом обработки правил
    /// </summary>
    [CollectionDataContract]
    [Serializable]
    [XmlRoot(OutputConstants.OutputWorshipChildNodeName)]
    public class OutputSectionModelCollection : List<OutputSectionModel>
    {
    }

}
