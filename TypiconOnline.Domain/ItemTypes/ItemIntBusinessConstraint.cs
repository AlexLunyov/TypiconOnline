using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemIntBusinessConstraint
    {
        public static readonly BusinessConstraint IntTypeMismatch = new BusinessConstraint("Неверный формат целого числа.");
    }
}
