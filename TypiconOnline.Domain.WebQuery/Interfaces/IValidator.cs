using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Interfaces
{
    public interface IValidator<T>
    {
        IEnumerable<ValidationResult> Validate(T instance);
    }
}
