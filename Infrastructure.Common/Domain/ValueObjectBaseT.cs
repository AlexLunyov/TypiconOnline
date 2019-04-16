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
                BrokenConstraints.Clear();
                Validate(validatePar);
                IsValidated = true;
            }

            return BrokenConstraints;
        }

        protected abstract void Validate(TValidatePar validatePar);
    }
}
