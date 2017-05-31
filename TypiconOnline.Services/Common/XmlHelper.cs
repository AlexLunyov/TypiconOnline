using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Common;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.AppServices.Common
{
    public class XmlHelper
    {
        public static ItemText CreateItemText(CreateItemTextRequest request)
        {
            ItemText itemText = new ItemText() { Name = request.Name };
            itemText.AddElement(request.Language, request.Text);
            itemText.Style = request.Style;

            return itemText;
        }
    }
}
