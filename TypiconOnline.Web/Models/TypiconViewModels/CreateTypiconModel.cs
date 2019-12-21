using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class CreateTypiconModel
    {
        public ItemText Name { get; set; }
        public ItemText Description { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина поля не должна превышать 20 символов")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]*$", ErrorMessage = "Поле может содержать только латинские символы")]
        public string SystemName { get; set; }

        public string DefaultLanguage { get; set; }

        public int TemplateId { get; set; }
    }
}
