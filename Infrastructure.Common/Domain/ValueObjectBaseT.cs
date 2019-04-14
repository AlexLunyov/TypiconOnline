using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public abstract class ValueObjectBase<TValidatePar> : ValidatableObject
    {
        public bool IsValid(TValidatePar validatePar) => GetBrokenConstraints(validatePar).Count == 0;

        public IReadOnlyCollection<BusinessConstraint> GetBrokenConstraints(TValidatePar validatePar)
        {
            if (!IsValidated)
            {
                _brokenConstraints.Clear();
                Validate(validatePar);
                IsValidated = true;
            }

            return _brokenConstraints;
        }

        protected abstract void Validate(TValidatePar validatePar);
    }
}
