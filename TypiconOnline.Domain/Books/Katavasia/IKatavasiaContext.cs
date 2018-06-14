namespace TypiconOnline.Domain.Books.Katavasia
{
    public interface IKatavasiaContext
    {
        GetKatavasiaResponse Get(GetKatavasiaRequest request);

        GetAllKatavasiaResponse GetAll();
    }
}
