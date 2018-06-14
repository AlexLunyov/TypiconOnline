using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.AppServices.Messaging.Common
{
    public class CreateItemTextRequest : ServiceRequestBase
    {
        public string Name;
        public string Text;
        public string Language = "cs-ru";
        public TextStyle Style = new TextStyle();
    }
}
