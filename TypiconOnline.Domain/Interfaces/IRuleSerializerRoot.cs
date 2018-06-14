using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleSerializerRoot
    {
        /// <summary>
        /// Единая точка для обработки запросов
        /// </summary>
        IDataQueryProcessor QueryProcessor { get; }
        RuleSerializerContainerBase<T> Container<T>() where T : IRuleElement;
        RuleSerializerContainerBase<T> Container<T, U>() where T : IRuleElement;
    }
}
