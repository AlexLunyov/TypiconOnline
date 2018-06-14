namespace TypiconOnline.Domain.Books.TheotokionApp
{
    public interface ITheotokionAppContext
    {
        GetTheotokionResponse Get(GetTheotokionRequest request);

        GetAllTheotokionResponse GetAll();
    }
}
