using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.WinServices.Messaging
{
    public class HandleTemplateResponse : ServiceResponseBase
    {
        public string Message { get; set; }
    }
}
