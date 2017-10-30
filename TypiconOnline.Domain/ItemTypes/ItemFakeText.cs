﻿using System;
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
    public class ItemFakeText /*: ItemStyledType, IXmlSerializable*/
    {
        protected Dictionary<string, string> _textDict = new Dictionary<string, string>();
        private string _stringExpression;

        public ItemFakeText() { }

        //public ItemFakeText(ItemText source) //: base(source)
        //{
        //    foreach (KeyValuePair<string, string> entry in source.Text)
        //    {
        //        AddElement(entry.Key, entry.Value);
        //    }
        //}

        public ItemFakeText(string expression) //: base(expression)
        {
            //Build(expression);
        }

        public ItemFakeText(XmlNode node)
        {
            //BuildFromXml(node);
        }

        #region Properties

        public Dictionary<string, string> Text
        {
            get
            {
                return _textDict;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(StringExpression);
            }
        }

        public bool IsTextEmpty
        {
            get
            {
                return _textDict.Count == 0;
            }
        }

        public string StringExpression
        {
            get
            {
                //_stringExpression = ComposeXml().InnerXml;
                return _stringExpression;
            }
            set
            {
                _stringExpression = value;
                //Build(value);
            }
        }

        public bool IsValid
        {
            get { return true; }
        }

        #endregion

        //protected override XmlDocument ComposeXml()
        //{
        //    XmlDocument doc = base.ComposeXml();

        //    foreach(KeyValuePair <string, string> entry in _textDict)
        //    {
        //        XmlNode node = doc.CreateElement(RuleConstants.ItemTextItemNode);

        //        XmlAttribute attr = doc.CreateAttribute(RuleConstants.ItemTextLanguageAttr);
        //        attr.Value = entry.Key;
        //        node.Attributes.Append(attr);

        //        node.InnerText = entry.Value;

        //        doc.FirstChild.AppendChild(node);
        //    }

        //    return doc;
        //}

        //protected override void Validate()
        //{
        //    foreach (KeyValuePair<string, string> entry in _textDict)
        //    {
        //        if (!IsKeyValid(entry.Key))
        //        {
        //            AddBrokenConstraint(ItemTextBusinessConstraint.LanguageMismatch, "ItemText");
        //        }
        //    }
        //}

        protected bool IsKeyValid(string key)
        {
            //Принимаем только элементы с именем xx-xx
            //TODO: добавить еще валидацию на предустановленные языки
            Regex rgx = new Regex(@"^[a-z]{2}-[a-z]{2}$");

            return rgx.IsMatch(key);
        }

        //protected override void Build(string expression)
        //{
        //    base.Build(expression);

        //    if (!string.IsNullOrEmpty(expression))
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(expression);

        //        if (doc?.DocumentElement != null)
        //        {
        //            XmlNode node = doc.DocumentElement;

        //            BuildFromXml(node);
        //        }
        //    }
            
        //}

        //protected virtual void BuildFromXml(XmlNode node)
        //{
        //    if (node.HasChildNodes)
        //    {
        //        _textDict.Clear();

        //        _isEmpty = true;

        //        foreach (XmlNode child in node.ChildNodes)
        //        {
        //            if (child.Name == RuleConstants.ItemTextItemNode)
        //            {
        //                XmlAttribute langAttr = child.Attributes[RuleConstants.ItemTextLanguageAttr];
        //                if (langAttr != null)
        //                {
        //                    string language = langAttr.Value;
        //                    AddElement(language, child.InnerText);

        //                    _isEmpty = false;
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Добавляет текст к коллекции
        /// </summary>
        /// <param name="key">язык</param>
        /// <param name="value">текст</param>
        /// <returns>Возвращает значение, успешно ли добавилось</returns>
        public bool AddElement(string key, string value)
        {
            StringExpression = value;

            return true;
        }

        /// <summary>
        /// Возвращает значение по заданному языку. Если результат нулевой, возвраает первый попавшийся вариант
        /// </summary>
        /// <param name="language">язык. Пример: "cs-ru"</param>
        /// <returns></returns>
        public string GetTextByLanguage(string language)
        {
            string result = "";

            if (_textDict.ContainsKey(language))
            {
                result = _textDict[language];// + " ";
            }
            else if (_textDict.Count > 0)
            {
                result = _textDict.Values.First();// + " ";
            }

            return result;

            //return (itemText.Text.ContainsKey(language)) 
            //    ? itemText.Text[language] + " " : "";
        }

        /// <summary>
        /// Возвращает значение по заданному языку. Если результат нулевой, возвраает первый попавшийся вариант
        /// </summary>
        /// <param name="language">язык. Пример: "cs-ru"</param>
        /// <returns></returns>
        public string this[string language]
        {
            get
            {
                return GetTextByLanguage(language);
            }
        }

        #region IXmlSerializable

        //public XmlSchema GetSchema()
        //{
        //    return null;
        //}

        //public virtual void ReadXml(XmlReader reader)
        //{
        //    bool wasEmpty = reader.IsEmptyElement;

        //    reader.MoveToElement();
        //    reader.Read();

        //    if (wasEmpty)
        //        return;

        //    while (reader.NodeType != XmlNodeType.EndElement)
        //    {
        //        reader.MoveToContent();

        //        string name = reader.Name;

        //        switch (name)
        //        {
        //            case RuleConstants.ItemTextItemNode:
        //                string language = reader.GetAttribute(RuleConstants.ItemTextLanguageAttr);
        //                if (IsKeyValid(language))
        //                {
        //                    string value = reader.ReadElementContentAsString();
        //                    AddElement(language, value);
        //                }
        //                //reader.MoveToElement();
        //                break;
        //            case RuleConstants.StyleNodeName:
        //                XmlSerializer _serializer = new XmlSerializer(typeof(TextStyle), new XmlRootAttribute(RuleConstants.StyleNodeName));
        //                Style = _serializer.Deserialize(reader) as TextStyle;
        //                break;
        //                //default:
        //                //    reader.Read();
        //                //    break;
        //        }
        //    }

        //    reader.Read();
        //}

        //public virtual void WriteXml(XmlWriter writer)
        //{
        //    foreach (KeyValuePair<string, string> entry in _textDict)
        //    {
        //        writer.WriteStartElement(RuleConstants.ItemTextItemNode);

        //        writer.WriteStartAttribute(RuleConstants.ItemTextLanguageAttr);
        //        writer.WriteValue(entry.Key);
        //        writer.WriteEndAttribute();

        //        writer.WriteString(entry.Value);

        //        writer.WriteEndElement();
        //    }

        //    if (Style != null && !base.IsEmpty)
        //    {
        //        XmlSerializer _serializer = new XmlSerializer(typeof(TextStyle), new XmlRootAttribute(RuleConstants.StyleNodeName));
        //        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //        ns.Add("", "");
        //        _serializer.Serialize(writer, Style, ns);
        //    }
        //}

        #endregion

        public override string ToString()
        {
            return _stringExpression;
        }
    }
}