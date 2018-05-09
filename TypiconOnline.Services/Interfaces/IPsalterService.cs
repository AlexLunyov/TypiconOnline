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
        AddPsalmResponse Add(AddPsalmRequest request);
        UpdatePsalmResponse Update(UpdatePsalmRequest request);
        RemovePsalmResponse Remove(RemovePsalmRequest request);
    }
}
