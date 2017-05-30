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
    public class ItemText : ItemStyledType
    {
        protected Dictionary<string, string> _textDict = new Dictionary<string, string>();

        public ItemText() { }

        public ItemText(string expression) : base(expression)
        {
            Build(expression);
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

            if ((doc != null) && (doc.DocumentElement != null))
            {
                XmlNode node = doc.DocumentElement;

                if (node.HasChildNodes)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name != RuleConstants.StyleNodeName)
                        {
                            AddElement(child.Name, child.InnerText);
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
    }
}
