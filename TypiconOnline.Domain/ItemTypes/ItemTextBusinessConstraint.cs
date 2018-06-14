using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemTextBusinessConstraint
    {
        public static readonly BusinessConstraint LanguageMismatch = new BusinessConstraint("Неверный формат языка.");
    }
}
