namespace TypiconOnline.Domain.Typicon.Factories
{
    public static class TypiconEntityFactory
    {
        public static TypiconEntity Create()
        {
            TypiconEntity typiconEntity = new TypiconEntity()
            {
                Name = "Типикон",
                DefaultLanguage = "cs-ru"
            };

            return typiconEntity;
        }
    }
}
