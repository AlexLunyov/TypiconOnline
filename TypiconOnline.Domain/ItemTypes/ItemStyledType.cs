using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ItemTypes
{
    public abstract class ItemStyledType : ItemType
    {
        protected TextStyle _style;

        public ItemStyledType()
        {
            _style = new TextStyle();
        }

        public ItemStyledType(TextStyle style)
        {
            if (style == null)
                throw new ArgumentNullException("Style in ItemText");
            _style = style;
        }

        public ItemStyledType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("Style");

            XmlNode child = node.SelectSingleNode(RuleConstants.StyleNodeName);

            if (child != null)
            {
                if (child.HasChildNodes)
                {
                    //парсим стиль
                    Style.IsBold = child.SelectSingleNode(RuleConstants.StyleBoldNodeName) != null;
                    Style.IsRed = child.SelectSingleNode(RuleConstants.StyleRedNodeName) != null;

                    foreach (string h in Enum.GetNames(typeof(HeaderCaption)))
                    {
                        if (child.SelectSingleNode(h) != null)
                        {
                            Style.Header = (HeaderCaption)Enum.Parse(typeof(HeaderCaption), h);
                        }
                    }
                }
            }
        }

        public TextStyle Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }
    }

    public class TextStyle
    {
        public bool IsRed = false;
        public bool IsBold = false;
        public HeaderCaption Header = HeaderCaption.NotDefined;
    }

    public enum HeaderCaption { NotDefined = 0, H1 = 1, H2 = 2, H3 = 3, H4 = 4 }
}
