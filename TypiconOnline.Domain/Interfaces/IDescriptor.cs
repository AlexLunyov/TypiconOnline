namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс, инкапсулирующий получение имени элемента правил
    /// </summary>
    public interface IDescriptor
    {
        string Description { get; set; }
        string GetElementName();
        IDescriptor CreateInstance(string description);
    }
}
