namespace TypiconOnline.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс 
    /// </summary>
    public interface IIntConverter
    {
        int Parse(string str);
        bool TryParse(string str, out int value);
        bool IsDigit(string str);
        string ToString(int value);
    }
}
