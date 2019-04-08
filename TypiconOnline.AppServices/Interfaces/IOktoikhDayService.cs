using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Books;
using TypiconOnline.Domain.Books.Oktoikh;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IOktoikhDayService : IOktoikhContext
    {
        AddOktoikhResponse Add(AddOktoikhRequest request);
        UpdateOktoikhResponse Update(UpdateOktoikhRequest request);
        RemoveOktoikhResponse Remove(RemoveOktoikhRequest request);
    }
}
