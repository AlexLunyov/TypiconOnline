using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.ItemTypes
{
    public class ItemTextCollection: ItemType
    {
        private string _stringExpression;

        private List<ItemText> _items = new List<ItemText>();

        public ItemTextCollection()
        {
            //Items = new List<ItemText>();
        }

        public ItemTextCollection(string expression) : this()
        {
            Build(expression);
        }

        public string TagName { get; set; }
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
        public ReadOnlyCollection<ItemText> Items
        {
            get
            {
                return _items.AsReadOnly();
            }
        }

        public void AddItem(ItemText item)
        {
            item.TagName = TagName + _items.Count;
            _items.Add(item);
        }

        private void Build(string expression)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(expression);

            if (doc?.DocumentElement != null)
            {
                XmlNode node = doc.DocumentElement;

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
        }

        private XmlDocument ComposeXml()
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
                foreach (BusinessConstraint constraint in item.GetBrokenConstraints())
                {
                    AddBrokenConstraint(constraint);
                }
            }
        }
    }
}
