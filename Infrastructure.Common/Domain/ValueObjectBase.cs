using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public abstract class ValueObjectBase: ValidatableObject
    {
        public ValueObjectBase()
        {
        }

        protected abstract void Validate();

        public bool IsValid => GetBrokenConstraints().Count == 0;

        public IReadOnlyCollection<BusinessConstraint> GetBrokenConstraints()
        {
            if (!IsValidated)
            {
                _brokenConstraints.Clear();
                Validate();
                IsValidated = true;
            }

            return _brokenConstraints;
        }

        public void ThrowExceptionIfInvalid()
        {
            if (!IsValid)
            {
                StringBuilder issues = new StringBuilder();
                foreach (BusinessConstraint businessConstraint in _brokenConstraints)
                {
                    issues.AppendLine(businessConstraint.ConstraintFullDescription);
                }

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }
    }
}
