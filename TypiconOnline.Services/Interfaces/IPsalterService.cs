using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Psalter;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IPsalterService : IPsalterContext
    {
        InsertPsalmResponse Insert(InsertPsalmRequest request);
        UpdatePsalmResponse Update(UpdatePsalmRequest request);
        DeletePsalmResponse Delete(DeletePsalmRequest request);
    }
}
