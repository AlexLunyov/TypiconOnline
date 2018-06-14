namespace TypiconOnline.Domain.Interfaces
{
    public interface IMergable<T> where T : class
    {
        void Merge(T source);
    }
}
