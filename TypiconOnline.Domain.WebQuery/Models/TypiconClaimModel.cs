using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconClaimModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина поля должна быть в диапазоне от 4 до 20 символов")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]*$", ErrorMessage = "Поле может содержать только латинские символы")]
        public string SystemName { get; set; }

        public string DefaultLanguage { get; set; } = CommonConstants.DefaultLanguage;

        public string ResultMessage { get; set; }

        public string Template { get; set; }
    }
}
