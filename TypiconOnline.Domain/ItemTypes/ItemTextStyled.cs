using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.ItemTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemTextStyled : ItemText
    {
        public ItemTextStyled() { }

        public ItemTextStyled(ItemTextStyled source) : base(source)
        {
            Style.Header = source.Style.Header;
            Style.IsBold = source.Style.IsBold;
            Style.IsRed = source.Style.IsRed;
        }

        public ItemTextStyled(string expression) : base(expression) { }

        #region Properties

        [XmlElement(RuleConstants.StyleNodeName)]
        public TextStyle Style { get; set; } = new TextStyle();

        public override bool IsEmpty
        {
            get
            {
                return base.IsEmpty && IsStyleEmpty;
            }
        }

        public bool IsStyleEmpty
        {
            get
            {
                return (!Style.IsRed && !Style.IsBold && Style.Header == HeaderCaption.NotDefined); ;
            }
        }

        #endregion

        #region Serialization

        //protected override XmlDocument ComposeXml()
        //{
        //    XmlDocument doc = base.ComposeXml();

        //    if (!IsStyleEmpty)
        //    {
        //        XmlNode styleNode = doc.CreateElement(RuleConstants.StyleNodeName);

        //        if (Style.IsRed)
        //        {
        //            styleNode.AppendChild(doc.CreateElement(RuleConstants.StyleRedNodeName));
        //        }

        //        if (Style.IsBold)
        //        {
        //            styleNode.AppendChild(doc.CreateElement(RuleConstants.StyleBoldNodeName));
        //        }

        //        if (Style.Header != HeaderCaption.NotDefined)
        //        {
        //            styleNode.AppendChild(doc.CreateElement(Enum.GetName(typeof(HeaderCaption), Style.Header)));
        //        }
        //        doc.FirstChild.AppendChild(styleNode);
        //    }

        //    return doc;
        //}


        //protected override void BuildFromXml(XmlNode node)
        //{
        //    base.BuildFromXml(node);

        //    XmlNode styleNode =  node.SelectSingleNode(RuleConstants.StyleNodeName);

        //    if (styleNode?.HasChildNodes == true)
        //    {
        //        //парсим стиль
        //        Style.IsBold = styleNode.SelectSingleNode(RuleConstants.StyleBoldNodeName) != null;
        //        Style.IsRed = styleNode.SelectSingleNode(RuleConstants.StyleRedNodeName) != null;

        //        XmlNode headerNode = styleNode.SelectSingleNode(RuleConstants.StyleHeaderNodeName);

        //        if (headerNode != null)
        //        {
        //            Enum.TryParse(headerNode.InnerText, out HeaderCaption caption);

        //            Style.Header = caption;
        //        }
        //    }
        //}

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);

            if (!IsStyleEmpty)
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(TextStyle), new XmlRootAttribute(RuleConstants.StyleNodeName));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                _serializer.Serialize(writer, Style, ns);
            }
        }

        protected override bool ReadNode(XmlReader reader)
        {
            bool isRead = base.ReadNode(reader);

            if (!isRead && RuleConstants.StyleNodeName == reader.Name)
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(TextStyle), new XmlRootAttribute(RuleConstants.StyleNodeName));
                Style = _serializer.Deserialize(reader) as TextStyle;

                isRead = true;
            }

            return isRead;
        }

        #endregion
    }
}