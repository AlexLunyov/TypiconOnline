using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class CreateTypiconModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Описание обязательно для заполнения")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Системное имя обязательно для заполнения")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина поля не должна превышать 20 символов")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]*$", ErrorMessage = "Поле может содержать только латинские символы")]
        [Remote(action: "IsNameUnique", controller: "Typicon")]
        public string SystemName { get; set; }

        public string DefaultLanguage { get; set; } = CommonConstants.DefaultLanguage;

        public int TemplateId { get; set; }
    }
}
