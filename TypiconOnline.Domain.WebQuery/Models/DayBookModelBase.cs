using System.ComponentModel.DataAnnotations;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public abstract class DayBookModelBase: BookModelBase
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsCelebrating { get; set; }
        public bool UseFullName { get; set; }
    }
}