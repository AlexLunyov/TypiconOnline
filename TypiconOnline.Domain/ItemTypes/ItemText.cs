using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemText : ItemStyledType
    {
        protected Dictionary<string, string> _textDict = new Dictionary<string, string>();

        public ItemText() { }

        public ItemText(XmlNode node) : base(node)
        {
            if (node == null)
                throw new ArgumentNullException("Node in ItemText");

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
            
        #region Properties

        public Dictionary<string, string> Text
        {
            get
            {
                return _textDict;
            }
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

        public void AddElement(string key, string value)
        {
            if (!_textDict.ContainsKey(key))
            {
                _textDict.Add(key, value);
            }
        }
    }

    
}
