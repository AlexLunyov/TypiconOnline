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
        protected TextStyle _style = new TextStyle();

        protected string _stringExpression;

        protected string _tagname = "itemstyled";

        public ItemStyledType() { }

        public ItemStyledType(string expression)
        {
            Build(expression);
        }

        public TextStyle Style = new TextStyle();/*
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }*/

        public string TagName = "itemstyled"; /*{
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }*/

        public virtual string StringExpression
        {
            get
            {
                _stringExpression = ComposeXml().InnerXml;
                return _stringExpression;
            }
            set
            {
                _stringExpression = value;
                Build(value);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(StringExpression);
            }
        }

        protected virtual XmlDocument ComposeXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.CreateElement(TagName);

            XmlNode styleNode = doc.CreateElement(RuleConstants.StyleNodeName);

            if (Style.IsRed)
            {
                styleNode.AppendChild(doc.CreateElement(RuleConstants.StyleRedNodeName));
            }

            if (Style.IsBold)
            {
                styleNode.AppendChild(doc.CreateElement(RuleConstants.StyleBoldNodeName));
            }

            if (Style.Header != HeaderCaption.NotDefined)
            {
                styleNode.AppendChild(doc.CreateElement(Enum.GetName(typeof(HeaderCaption), Style.Header)));
            }
            root.AppendChild(styleNode);
            doc.AppendChild(root);

            return doc;
        }

        protected virtual void Build(string expression)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(expression);

            if ((doc != null) && (doc.DocumentElement != null))
            {
                XmlNode node = doc.DocumentElement;

                TagName = node.Name;

                node = node.SelectSingleNode(RuleConstants.StyleNodeName);

                if ((node != null) && (node.HasChildNodes))
                {
                    //парсим стиль
                    Style.IsBold = node.SelectSingleNode(RuleConstants.StyleBoldNodeName) != null;
                    Style.IsRed = node.SelectSingleNode(RuleConstants.StyleRedNodeName) != null;

                    foreach (string h in Enum.GetNames(typeof(HeaderCaption)))
                    {
                        if (node.SelectSingleNode(h.ToLower()) != null)
                        {
                            Style.Header = (HeaderCaption)Enum.Parse(typeof(HeaderCaption), h);
                        }
                    }
                }
            }
        }
    }

    public class TextStyle
    {
        public bool IsRed = false;
        public bool IsBold = false;
        public HeaderCaption Header = HeaderCaption.NotDefined;
    }

    public enum HeaderCaption { NotDefined = 0, h1 = 1, h2 = 2, h3 = 3, h4 = 4 }
}
