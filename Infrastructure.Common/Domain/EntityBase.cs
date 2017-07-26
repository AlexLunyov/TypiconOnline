using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public abstract class EntityBase<IdType> : ValueObjectBase
    {
        //private List<BusinessConstraint> _brokenConstraints = new List<BusinessConstraint>();

        public IdType Id { get; set; }

        //protected abstract void Validate();

        //protected virtual bool IsValidated { get; set; }

        //public bool IsValid
        //{
        //    get
        //    {
        //        return (GetBrokenConstraints().Count() == 0);
        //        //return (_brokenRules.Count() == 0);
        //    }
        //}

        //protected void AddBrokenConstraint(BusinessConstraint businessConstraint)
        //{
        //    _brokenConstraints.Add(businessConstraint);
        //}

        //protected void AddBrokenConstraint(BusinessConstraint businessConstraint, string principlePath)
        //{
        //    businessConstraint.ConstraintPath = principlePath + "." + businessConstraint.ConstraintPath;
        //    _brokenConstraints.Add(businessConstraint);
        //}

        ///// <summary>
        ///// Добавляет все ломаные правила элемента к себе в коллекцию
        ///// </summary>
        ///// <param name="element"></param>
        ///// <param name="name"></param>
        //protected void AppendAllBrokenConstraints(ValueObjectBase element, string name)
        //{
        //    foreach (BusinessConstraint brokenBR in element.GetBrokenConstraints())
        //    {
        //        AddBrokenConstraint(brokenBR, name);
        //    }
        //}

        //protected void AppendAllBrokenConstraints(ValueObjectBase element)
        //{
        //    AppendAllBrokenConstraints(element, string.Empty);
        //}

        //public IEnumerable<BusinessConstraint> GetBrokenConstraints()
        //{
        //    if (!IsValidated)
        //    {
        //        _brokenConstraints.Clear();
        //        Validate();
        //        IsValidated = true;
        //    }
        //    return _brokenConstraints;
        //}

        public override bool Equals(object entity)
        {
            return entity != null
               && entity is EntityBase<IdType>
               && this == (EntityBase<IdType>)entity;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<IdType> entity1, EntityBase<IdType> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(EntityBase<IdType> entity1, EntityBase<IdType> entity2)
        {
            return (!(entity1 == entity2));
        }
    }
}
