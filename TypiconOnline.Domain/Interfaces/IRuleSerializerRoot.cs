using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleSerializerRoot
    {
        /// <summary>
        /// Единая точка для обработки запросов
        /// </summary>
        IDataQueryProcessor QueryProcessor { get; }
        /// <summary>
        /// Сериализатор для богослужебных текстов
        /// </summary>
        ITypiconSerializer TypiconSerializer { get; }
        IRuleSerializerContainer<T> Container<T>() where T : IRuleElement;
        IRuleSerializerContainer<T> Container<T, U>() where T : IRuleElement;
    }
}
