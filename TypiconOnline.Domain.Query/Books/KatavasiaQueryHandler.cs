using JetBrains.Annotations;
using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Exceptions;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Books
{
    public class KatavasiaQueryHandler : DbContextQueryBase, IDataQueryHandler<KatavasiaQuery, Kanonas>
    {
        readonly ITypiconSerializer typiconSerializer;

        public KatavasiaQueryHandler(TypiconDBContext dbContext, [NotNull] ITypiconSerializer typiconSerializer) : base(dbContext)
        {
            this.typiconSerializer = typiconSerializer;
        }

        public Kanonas Handle([NotNull] KatavasiaQuery query)
        {
            var katavasia = DbContext.Set<Katavasia>().FirstOrDefault(c => c.Name == query.Name);

            if (katavasia == null)
            {
                throw new ResourceNotFoundException("Катавасия");
            }

            var kanonas = katavasia.GetElement(typiconSerializer);

            if (kanonas == null)
            {
                //exception too
            }

            return kanonas;
        }
    }
}
