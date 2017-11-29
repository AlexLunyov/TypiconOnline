using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.WinServices.Messaging;

namespace TypiconOnline.WinServices.Interfaces
{
    public interface IDocxTemplateService
    {
        HandleTemplateResponse Operate(HandleTemplateRequest request);
    }
}
