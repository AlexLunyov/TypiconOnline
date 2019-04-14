using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
//using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain
{
    public abstract class BookElementBase<T> : ValueObjectBase<ITypiconSerializer>, IHasId<int>, IBookElement<T> where T : DayElementBase
    {
        public int Id { get; set; }

        public virtual string Definition { get; set; }

        public virtual T GetElement(ITypiconSerializer serializer)
        {
            return serializer.Deserialize<T>(Definition);
        }

        protected override void Validate(ITypiconSerializer typiconSerializer)
        {
            if (string.IsNullOrEmpty(Definition))
            {
                AddBrokenConstraint(BookElementBaseBusinessConstraint.EmptyStringDefinition);
            }
            else
            {
                var element = GetElement(typiconSerializer);
                if (element == null)
                {
                    AddBrokenConstraint(new BusinessConstraint("Правило заполнено с неопределяемыми системой ошибками.", "Definition"));
                }
                else if (!element.IsValid)
                {
                    AppendAllBrokenConstraints(element.GetBrokenConstraints(), "Definition");
                }
            }
        }
    }

    public class BookElementBaseBusinessConstraint
    {
        public static readonly BusinessConstraint EmptyStringDefinition = new BusinessConstraint("Отсутствует определение песнопения.");
    }
}
