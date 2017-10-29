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
    public class ItemText : ItemType, IXmlSerializable
    {
        private Dictionary<string, string> _textDict = new Dictionary<string, string>();

        public ItemText() { }

        public ItemText(ItemText source)
        {
            foreach (KeyValuePair<string, string> entry in source._textDict)
            {
                AddElement(entry.Key, entry.Value);
            }
        }

        public ItemText(string expression)
        {
            Build(expression);
        }

        #region Properties

        protected string TagName {get; private set; } = "text"; 

        public virtual bool IsEmpty
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
                return ComposeXml().InnerXml;
            }
            set
            {
                Build(value);
            }
        }

        #endregion


        protected override void Validate()
        {
            foreach (KeyValuePair<string, string> entry in _textDict)
            {
                if (!IsKeyValid(entry.Key))
                {
                    AddBrokenConstraint(ItemTextBusinessConstraint.LanguageMismatch, "ItemText");
                }
            }
        }

        protected bool IsKeyValid(string key)
        {
            //Принимаем только элементы с именем xx-xx
            //TODO: добавить еще валидацию на предустановленные языки
            Regex rgx = new Regex(@"^[a-z]{2}-[a-z]{2}$");

            return rgx.IsMatch(key);
        }

        protected virtual XmlDocument ComposeXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.CreateElement(TagName);
            doc.AppendChild(root);

            foreach (KeyValuePair<string, string> entry in _textDict)
            {
                XmlNode node = doc.CreateElement(RuleConstants.ItemTextItemNode);

                XmlAttribute attr = doc.CreateAttribute(RuleConstants.ItemTextLanguageAttr);
                attr.Value = entry.Key;
                node.Attributes.Append(attr);

                node.InnerText = entry.Value;

                root.AppendChild(node);
            }

            return doc;
        }

        protected void Build(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(expression);

                if (doc?.DocumentElement != null)
                {
                    BuildFromXml(doc.DocumentElement);
                }
            }
        }

        protected virtual void BuildFromXml(XmlNode node)
        {
            TagName = node.Name;

            if (node.HasChildNodes)
            {
                _textDict.Clear();

                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name == RuleConstants.ItemTextItemNode)
                    {
                        XmlAttribute langAttr = child.Attributes[RuleConstants.ItemTextLanguageAttr];
                        if (langAttr != null)
                        {
                            string language = langAttr.Value;
                            AddElement(language, child.InnerText);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Добавляет текст к коллекции
        /// </summary>
        /// <param name="key">язык</param>
        /// <param name="value">текст</param>
        /// <returns>Возвращает значение, успешно ли добавилось</returns>
        public bool AddElement(string key, string value)
        {
            bool result = false;
            if (!_textDict.ContainsKey(key))
            {
                _textDict.Add(key, value);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Возвращает значение по заданному языку. Если результат нулевой, возвраает первый попавшийся вариант
        /// </summary>
        /// <param name="language">язык. Пример: "cs-ru"</param>
        /// <returns></returns>
        private string GetTextByLanguage(string language)
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
            set
            {
                _textDict[language] = value;
            }
        }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            if (doc.HasChildNodes)
            {
                BuildFromXml(doc.DocumentElement);
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            ComposeXml().DocumentElement.WriteContentTo(writer);
        }

        #endregion

        public override string ToString()
        {
            return (_textDict.Count > 0) ? _textDict.First().Value : base.ToString();
        }
    }
}