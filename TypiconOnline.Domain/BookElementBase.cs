using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    public abstract class BookElementBase<T> : EntityBase<int>, IAggregateRoot, IBookElement<T> where T : DayElementBase
    {
        protected T _element;

        public virtual string Definition { get; set; }

        public T GetElement()
        {
            return GetElement(new TypiconSerializer());
        }

        public T GetElement(IXmlSerializer serializer)
        {
            //ThrowExceptionIfInvalid();

            if (_element == null)
            {
                _element = serializer.Deserialize<T>(Definition);
            }

            return _element;
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Definition))
            {
                AddBrokenConstraint(BookElementBaseBusinessConstraint.EmptyStringDefinition);
            }
        }
    }

    public class BookElementBaseBusinessConstraint
    {
        public static readonly BusinessConstraint EmptyStringDefinition = new BusinessConstraint("Отсутствует определение песнопения.");
    }
}
