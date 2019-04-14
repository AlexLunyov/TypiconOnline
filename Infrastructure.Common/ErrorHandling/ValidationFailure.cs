using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.ErrorHandling
{
    public class ValidationFailure : Failure
    {
        public ValidationResult[] ValidationResults { get; }

        public ValidationFailure(IEnumerable<ValidationResult> validationResults)
            : base(ValidationResultsToStrings(validationResults))
        {
            ValidationResults = validationResults?.ToArray();
            if (ValidationResults == null || !ValidationResults.Any())
            {
                throw new ArgumentException(nameof(validationResults));
            }


            Data = new ReadOnlyDictionary<string, object>(
                ValidationResults.ToDictionary(
                x => string.Join(",", x.MemberNames),
                x => (object)x.ErrorMessage));
        }

        private static string ValidationResultsToStrings(
            IEnumerable<ValidationResult> validationResults)
            => string.Join(Environment.NewLine, validationResults.Select(x => x.ErrorMessage));
    }
}
