using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        
        [Required]
        [EmailAddress]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Поле {0} должно иметь хотя бы {2} и максимально - {1} символов.", MinimumLength = 4)]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь хотя бы {2} и максимально - {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
