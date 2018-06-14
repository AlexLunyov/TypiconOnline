using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    public abstract class BookElementBase<T> : EntityBase<int>, IBookElement<T> where T : DayElementBase
    {
        protected T _element;

        public virtual string Definition { get; set; }

        public virtual T GetElement()
        {
            return GetElement(new TypiconSerializer());
        }

        public virtual T GetElement(ITypiconSerializer serializer)
        {
            //ThrowExceptionIfInvalid();

            //не проверяем, так как каждый раз выдаем копию элемента, иначе изменения, 
            //которые неизбежно вносят элементы правила - сохраняются
            //if (_element == null)
            //{
                _element = serializer.Deserialize<T>(Definition);
            //}

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
