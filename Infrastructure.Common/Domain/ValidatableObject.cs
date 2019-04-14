using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    /// <summary>
    /// Базовый класс для всех доменных классов, содержащих механизм для валидации
    /// </summary>
    public abstract class ValidatableObject
    {
        private protected readonly ConstraintsCollection _brokenConstraints = new ConstraintsCollection();

        /// <summary>
        /// Признак того, был ли объект валидирован.
        /// Необходимо обнулять его при каждом изменении внутренних свойств
        /// </summary>
        protected bool IsValidated { get; set; } = false;

        protected void AddBrokenConstraint(BusinessConstraint businessConstraint, string constraintPath = null)
            => _brokenConstraints.AddBrokenConstraint(businessConstraint, constraintPath);

        /// <summary>
        /// Добавляет все ломаные правила элемента к себе в коллекцию
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        protected void AppendAllBrokenConstraints(ValueObjectBase element, string name)
            => _brokenConstraints.AppendAllBrokenConstraints(element, name);

        protected void AppendAllBrokenConstraints<T>(ValueObjectBase<T> element, T validator, string name)
            => _brokenConstraints.AppendAllBrokenConstraints(element, validator, name);

        protected void AppendAllBrokenConstraints(IReadOnlyCollection<BusinessConstraint> businessConstraints, string name = null)
            => _brokenConstraints.AppendAllBrokenConstraints(businessConstraints, name);

        protected void AppendAllBrokenConstraints(ValueObjectBase element)
            => _brokenConstraints.AppendAllBrokenConstraints(element);

        protected void AppendAllBrokenConstraints<T>(ValueObjectBase<T> element, T validator)
            => _brokenConstraints.AppendAllBrokenConstraints(element, validator);
    }
}
