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
                BrokenConstraints.Clear();
                Validate();
                IsValidated = true;
            }

            return BrokenConstraints;
        }

        public void ThrowExceptionIfInvalid()
        {
            if (!IsValid)
            {
                StringBuilder issues = new StringBuilder();
                foreach (BusinessConstraint businessConstraint in BrokenConstraints)
                {
                    issues.AppendLine(businessConstraint.ConstraintFullDescription);
                }

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }
    }
}
