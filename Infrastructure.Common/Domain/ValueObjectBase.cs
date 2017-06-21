using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public abstract class ValueObjectBase
    {
        private List<BusinessConstraint> _brokenConstraints = new List<BusinessConstraint>();

        protected bool _isValidated = false;

        public ValueObjectBase()
        {
        }

        protected abstract void Validate();

        public bool IsValid {
            get
            {
                return (GetBrokenConstraints().Count() == 0);
            }
        }

        public List<BusinessConstraint> GetBrokenConstraints()
        {
            if (!_isValidated)
            {
                _brokenConstraints.Clear();
                Validate();
                _isValidated = true;
            }

            return _brokenConstraints;
        }

        public void ThrowExceptionIfInvalid()
        {
            //_brokenRules.Clear();
            //Validate();
            if (_brokenConstraints.Count() > 0)
            {
                StringBuilder issues = new StringBuilder();
                foreach (BusinessConstraint businessRule in _brokenConstraints)
                {
                    issues.AppendLine(businessRule.ConstraintFullDescription);
                }

                throw new ValueObjectIsInvalidException(issues.ToString());
            }
        }

        protected void AddBrokenConstraint(BusinessConstraint businessConstraint)
        {
            _brokenConstraints.Add(businessConstraint);
        }

        protected void AddBrokenConstraint(BusinessConstraint businessConstraint, string constraintPath)
        {
            businessConstraint.ConstraintPath = constraintPath/* + "." + businessConstraint.ConstraintPath*/;
            _brokenConstraints.Add(businessConstraint);
        }

        /// <summary>
        /// Добавляет все ломаные правила элемента к себе в коллекцию
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        protected void AppendAllBrokenConstraints(ValueObjectBase element, string name)
        {
            foreach (BusinessConstraint brokenBR in element.GetBrokenConstraints())
            {
                AddBrokenConstraint(brokenBR, name);
            }
        }

        protected void AppendAllBrokenConstraints(ValueObjectBase element)
        {
            AppendAllBrokenConstraints(element, string.Empty);
        }
    }
}
