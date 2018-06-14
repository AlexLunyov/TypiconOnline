namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс, инкапсулирующий получение имени элемента правил
    /// </summary>
    public interface IDescriptor<T> : IDescriptor where T: class
    {
        T Element { get; set; }
    }
}
