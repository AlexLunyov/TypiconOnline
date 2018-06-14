using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemEnumTypeBusinessConstraint
    {
        public static readonly BusinessConstraint EnumTypeMismatch = new BusinessConstraint("Неверный формат перечисления.");
    }
}
