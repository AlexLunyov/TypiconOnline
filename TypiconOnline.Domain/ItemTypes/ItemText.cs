using System;
using System.Collections.Generic;
using System.IO;
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
            StringExpression = expression;
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

        /// <summary>
        /// Строкове выражение многоязычного текста
        /// </summary>
        public string StringExpression
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                using (XmlWriter writer = XmlWriter.Create(builder))
                {
                    writer.WriteStartElement(TagName);

                    WriteXml(writer);

                    writer.WriteEndElement();
                    writer.Close();

                    return builder.ToString();
                }
            }
            set
            {
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(value)))
                {
                    bool wasEmpty = xmlReader.IsEmptyElement;

                    xmlReader.MoveToElement();
                    xmlReader.Read();

                    if (!wasEmpty)
                    {
                        ReadXml(xmlReader);
                    }
                }
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
        public string this[string language]
        {
            get
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
            bool wasEmpty = reader.IsEmptyElement;

            reader.MoveToElement();
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.MoveToContent();

                ReadNode(reader);
            }

            reader.Read();
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            foreach (KeyValuePair<string, string> entry in _textDict)
            {
                writer.WriteStartElement(RuleConstants.ItemTextItemNode);

                writer.WriteStartAttribute(RuleConstants.ItemTextLanguageAttr);
                writer.WriteValue(entry.Key);
                writer.WriteEndAttribute();

                writer.WriteString(entry.Value);

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Метод, который должен переопределяться у наследников, в котором будет совершаться десериализация их собственных свойств
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual bool ReadNode(XmlReader reader)
        {
            bool isRead = false;

            //string nodeName = reader.Name;

            if (RuleConstants.ItemTextItemNode == reader.Name)
            {
                string language = reader.GetAttribute(RuleConstants.ItemTextLanguageAttr);
                string value = reader.ReadElementContentAsString();
                if (IsKeyValid(language))
                {
                    AddElement(language, value);
                }

                isRead = true;
            }
            return isRead;
        }

        #endregion

        public override string ToString()
        {
            return (_textDict.Count > 0) ? _textDict.First().Value : base.ToString();
        }
    }
}