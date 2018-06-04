using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Core.Interfaces
{
    public interface IRuleSerializerRoot
    {
        //BookStorage BookStorage { get; }

        /// <summary>
        /// Единая точка для обработки запросов
        /// </summary>
        IDataQueryProcessor QueryProcessor { get; }
        RuleSerializerContainerBase<T> Container<T>() where T : class, IRuleElement;
        RuleSerializerContainerBase<T> Container<T, U>() where T : class, IRuleElement;
    }
}
