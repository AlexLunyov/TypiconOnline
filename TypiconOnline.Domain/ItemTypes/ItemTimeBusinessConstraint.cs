using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemTimeBusinessConstraint
    {
        public static readonly BusinessConstraint TimeTypeMismatch = new BusinessConstraint("Неверный формат времени.");
    }
}
