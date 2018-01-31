using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Typicon.Modifications
{
    /// <summary>
    /// Фильтр для служб Правила
    /// </summary>
    public class DayWorshipsFilter : ValueObjectBase
    {
        public int? ExcludedItem { get; set; }
        public int? IncludedItem { get; set; }
        public bool? IsCelebrating { get; set; }

        protected override void Validate()
        {
            if (ExcludedItem != null
                && IncludedItem != null
                && ExcludedItem == IncludedItem)
            {
                AddBrokenConstraint(DayWorshipsFilterBusinessConstraint.PropertiesHasSameValue);
            }
        }
    }

    public class DayWorshipsFilterBusinessConstraint
    {
        public static readonly BusinessConstraint PropertiesHasSameValue = new BusinessConstraint("Исключенная и включенная служба не может иметь одно и то же значение.");
    }
}