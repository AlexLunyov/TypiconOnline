using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public class ConstraintsCollection : List<BusinessConstraint>
    {
        public void AddBrokenConstraint(BusinessConstraint businessConstraint, string constraintPath = null)
        {
            if (!string.IsNullOrEmpty(constraintPath))
            {
                businessConstraint.ConstraintPath = (string.IsNullOrEmpty(businessConstraint.ConstraintPath))
                ? constraintPath : $"{constraintPath}.{businessConstraint.ConstraintPath}";
            }

            Add(businessConstraint);
        }

        /// <summary>
        /// Добавляет все ломаные правила элемента к себе в коллекцию
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        public void AppendAllBrokenConstraints(ValueObjectBase element, string name = null)
        {
            foreach (BusinessConstraint brokenBR in element.GetBrokenConstraints())
            {
                AddBrokenConstraint(brokenBR, name);
            }
        }

        public void AppendAllBrokenConstraints<T>(ValueObjectBase<T> element, T validator, string name = null)
        {
            foreach (BusinessConstraint brokenBR in element.GetBrokenConstraints(validator))
            {
                AddBrokenConstraint(brokenBR, name);
            }
        }

        public void AppendAllBrokenConstraints(IReadOnlyCollection<BusinessConstraint> businessConstraints, string name = null)
        {
            foreach (BusinessConstraint brokenBR in businessConstraints)
            {
                AddBrokenConstraint(brokenBR, name);
            }
        }
    }
}
