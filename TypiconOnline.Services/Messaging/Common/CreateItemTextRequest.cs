using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Messaging.Common
{
    public class CreateItemTextRequest
    {
        public string Name;
        public string Text;
        public string Language = "cs-ru";
        public TextStyle Style = new TextStyle();
    }
}
