using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
//using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    public abstract class BookElementBase<T> : EntityBase<int>, IBookElement<T> where T : DayElementBase
    {
        public virtual string Definition { get; set; }

        public virtual T GetElement(ITypiconSerializer serializer)
        {
            return serializer.Deserialize<T>(Definition);
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
