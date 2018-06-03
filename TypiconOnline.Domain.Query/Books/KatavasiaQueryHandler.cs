using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Exceptions;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    public class KatavasiaQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<KatavasiaQuery, Kanonas>
    {
        ITypiconSerializer typiconSerializer;

        public KatavasiaQueryHandler(IUnitOfWork unitOfWork, [NotNull] ITypiconSerializer typiconSerializer) : base(unitOfWork)
        {
            this.typiconSerializer = typiconSerializer;
        }

        public Kanonas Handle([NotNull] KatavasiaQuery query)
        {
            var katavasia = UnitOfWork.Repository<Katavasia>().Get(c => c.Name == query.Name);

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
