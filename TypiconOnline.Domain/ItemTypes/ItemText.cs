using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.ItemTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemText : ItemStyledType
    {
        protected Dictionary<string, string> _textDict = new Dictionary<string, string>();

        public ItemText() { }

        public ItemText(string expression) : base(expression)
        {
            Build(expression);
        }

        public ItemText(XmlNode node)
        {
            BuildFromXml(node);
        }

        #region Properties

        public Dictionary<string, string> Text
        {
            get
            {
                return _textDict;
            }
        }

        //public override string StringExpression
        //{
        //    get
        //    {
        //        _stringExpression = ComposeXml().Value;
        //        return _stringExpression;
        //    }
        //    set
        //    {
        //        _stringExpression = value;
        //        Build(value);
        //    }
        //}

        protected override XmlDocument ComposeXml()
        {
            XmlDocument doc = base.ComposeXml();

            foreach(KeyValuePair <string, string> entry in _textDict)
            {
                XmlNode node = doc.CreateElement(entry.Key);
                node.InnerText = entry.Value;
                doc.FirstChild.AppendChild(node);
            }

            return doc;
        }

        #endregion


        protected override void Validate()
        {
            //Принимаем только элементы с именем xx-xx
            //TODO: добавить еще валидацию на предустановленные языки
            Regex rgx = new Regex(@"^[a-z]{2}-[a-z]{2}$");
            
            foreach (KeyValuePair<string, string> entry in _textDict)
            {
                if (!rgx.IsMatch(entry.Key))
                {
                    AddBrokenConstraint(ItemTextBusinessConstraint.LanguageMismatch, "ItemText");
                }
            }
        }

        protected override void Build(string expression)
        {
            base.Build(expression);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(expression);

            if (doc?.DocumentElement != null)
            {
                XmlNode node = doc.DocumentElement;

                BuildFromXml(node);
            }
        }

        private void BuildFromXml(XmlNode node)
        {
            if (node.HasChildNodes)
            {
                _textDict.Clear();

                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name != RuleConstants.StyleNodeName)
                    {
                        AddElement(child.Name, child.InnerText);
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
    }
}
