using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ItemTypes
{
    public abstract class ItemStyledType : ItemType
    {
        protected string _stringExpression;

        protected string _tagname = "itemstyled";

        public ItemStyledType() { }

        public ItemStyledType(ItemStyledType source)
        {
            StringExpression = source.StringExpression;
        }

        public ItemStyledType(string expression)
        {
            StringExpression = expression;
        }

        [XmlElement(RuleConstants.StyleNodeName)]
        public TextStyle Style { get; set; } = new TextStyle();

        [XmlIgnore]
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
        [XmlIgnore]
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

        protected bool _isEmpty = true;

        /// <summary>
        /// Возвращает True, если хотя бы одно из свойств не имеет значение по умолчанию
        /// </summary>
        public virtual bool IsEmpty
        {
            get
            {
                return (!Style.IsRed && !Style.IsBold && Style.Header == HeaderCaption.NotDefined);

                //return _isEmpty;// string.IsNullOrEmpty(StringExpression);
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
            if (!string.IsNullOrEmpty(_stringExpression))
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

                        _isEmpty = false;
                    }
                    else
                    {
                        _isEmpty = true;
                    }
                }
            }
        }

        public virtual bool Equals(ItemStyledType item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("ItemStyledType.Equals");
            }
            return (_stringExpression == item._stringExpression);
        }
    }
    
}
