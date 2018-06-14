using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemBooleanBusinessConstraint
    {
        public static readonly BusinessConstraint BooleanTypeMismatch = new BusinessConstraint("Неверный формат логического обозначения.");
    }
}
