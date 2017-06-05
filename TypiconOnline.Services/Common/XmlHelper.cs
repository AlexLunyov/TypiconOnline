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
            ItemText itemText = new ItemText() { TagName = request.Name };
            itemText.AddElement(request.Language, request.Text);
            itemText.Style = request.Style;

            return itemText;
        }

        public static ItemTextCollection CreateItemTextCollection(CreateItemTextRequest request)
        {
            ItemTextCollection col = new ItemTextCollection()
            {
                TagName = request.Name,
            };

            ItemText itemText = new ItemText();
            itemText.AddElement(request.Language, request.Text);
            itemText.Style = request.Style;

            col.AddItem(itemText);

            return col;
        }
    }
}
