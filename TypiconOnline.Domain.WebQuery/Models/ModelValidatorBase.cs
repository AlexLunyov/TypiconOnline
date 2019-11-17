using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Модель для валидации</typeparam>
    public abstract class ModelValidatorBase<T> : IValidator<T> where T : class, new()
    {
        public abstract IEnumerable<ValidationResult> Validate(T instance);
    }
}
