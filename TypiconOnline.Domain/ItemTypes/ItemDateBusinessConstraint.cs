using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemDateBusinessConstraint
    {
        public static readonly BusinessConstraint DateTypeMismatch = new BusinessConstraint("Неверный формат даты.");
    }
}
