using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    class ItemDayOfWeekBusinessConstraint
    {
        public static readonly BusinessConstraint DayOfWeekTypeMismatch = new BusinessConstraint("Неверный формат дня недели.");
    }
}
