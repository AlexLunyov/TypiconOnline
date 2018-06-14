namespace TypiconOnline.Domain.Books.Psalter
{
    public interface IPsalterContext
    {
        GetPsalmResponse Get(GetPsalmRequest request);
    }
}
