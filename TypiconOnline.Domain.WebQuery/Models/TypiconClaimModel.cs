using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconClaimModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина поля не должна превышать 20 символов")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]*$", ErrorMessage = "Поле может содержать только латинские символы")]
        public string SystemName { get; set; }

        public string DefaultLanguage { get; set; }

        public string ResultMessage { get; set; }

        public string Template { get; set; }
    }
}
