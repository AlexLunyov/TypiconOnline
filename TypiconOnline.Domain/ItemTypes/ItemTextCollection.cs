using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemTextCollection: ItemType
    {
        private string _stringExpression;

        

        public ItemTextCollection()
        {
            //Items = new List<ItemText>();
        }

        public ItemTextCollection(XmlNode node) : this()
        {
            BuildFromXml(node);
        }

        public ItemTextCollection(string expression) : this()
        {
            Build(expression);
        }
        [XmlIgnore]
        public string TagName { get; private set; }
        [XmlIgnore]
        public virtual string StringExpression
        {
            get
            {
                return ComposeXml().OuterXml;
            }
            set
            {
                _stringExpression = value;
                Build(_stringExpression);
            }
        }

        private List<ItemText> _items = new List<ItemText>();

        [XmlElement(RuleConstants.ItemTextCollectionNode)]
        public List<ItemText> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        private void Build(string expression)
        {
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
            TagName = node.Name;

            if (node.HasChildNodes)
            {
                _items.Clear();

                foreach (XmlNode child in node.ChildNodes)
                {
                    _items.Add(new ItemText(child.OuterXml));
                }
            }
        }

        protected virtual XmlDocument ComposeXml()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.CreateElement(TagName);
            doc.AppendChild(root);

            foreach (ItemText item in Items)
            {
                root.InnerXml += item.StringExpression;
            }

            return doc;
        }

        protected override void Validate()
        {
            foreach (ItemText item in Items)
            {
                AppendAllBrokenConstraints(item);
            }
        }
    }
}
