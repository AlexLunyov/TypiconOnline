using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.WebQuery.Extensions
{
    public static class ValidateExtensions
    {
        /// <summary>
        /// Проверка ItemText, которое обязательно к заполнению
        /// </summary>
        /// <param name="itemText"></param>
        /// <param name="propertyName">Имя свойства для отображения</param>
        /// <param name="propertyViewName">Имя свойства для описания в тексте ошибок</param>
        /// <param name="errors">список ошибок</param>
        public static void ValidateRequired(this ItemText itemText, string propertyName, string propertyViewName, List<ValidationResult> errors)
        {
            if (itemText == null
                || !itemText.Languages.Contains(CommonConstants.DefaultLanguage))
            {
                errors.Add(new ValidationResult($"{propertyViewName} должно быть определено на языке \"{CommonConstants.DefaultLanguage}\"", new List<string>() { propertyName }));
            }

            if (itemText?.IsValid == false)
            {
                errors.Add(new ValidationResult($"{propertyViewName} заполнено с неверным определением языка", new List<string>() { propertyName }));
            }
        }

        /// <summary>
        /// Проверка ItemText, которое не обязательно к заполнению
        /// </summary>
        /// <param name="itemText"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyViewName"></param>
        /// <param name="errors"></param>
        public static void ValidateNotRequired(this ItemText itemText, string propertyName, string propertyViewName, List<ValidationResult> errors)
        {
            if (itemText?.IsEmpty == false)
            {
                if (!itemText.Languages.Contains(CommonConstants.DefaultLanguage))
                {
                    errors.Add(new ValidationResult($"{propertyViewName} должно быть определено на языке \"{CommonConstants.DefaultLanguage}\"", new List<string>() { propertyName }));
                }

                if (!itemText.IsValid)
                {
                    errors.Add(new ValidationResult($"{propertyViewName} заполнено с неверным определением языка", new List<string>() { propertyName }));
                }
            }
        }
    }
}
