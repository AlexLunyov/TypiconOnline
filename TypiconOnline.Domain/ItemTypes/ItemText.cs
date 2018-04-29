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
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemText : ItemType
    {
        private List<ItemTextUnit> items = new List<ItemTextUnit>();
        

        public ItemText() : this(new TypiconSerializer()) { }

        public ItemText(ITypiconSerializer serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException("serializer in ItemText");
        }

        public ItemText(string exp) : this() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp">xml-выражение</param>
        /// <param name="rootName">корневой элемент</param>
        public ItemText(string exp, string rootName) : this()
        {
            RootName = rootName;

            StringExpression = exp;
        }

        public ItemText(ItemText source) : this()
        {
            if (source == null) throw new ArgumentNullException("source in ItemText");

            RootName = source.RootName;

            Build(source);
        }

        protected ITypiconSerializer Serializer { get; }

        protected string RootName { get; } //= RuleConstants.ItemTextDefaultNode;

        [XmlElement("item")]
        public ItemTextUnit[] Items
        {
            get
            {
                return items.ToArray();
            }
            set
            {
                if (value != null)
                {
                    items = value.ToList();
                }
            }
        }

        public virtual bool IsEmpty => items.Count == 0;

        public IEnumerable<string> Languages => items.Select(c => c.Language);

        [XmlIgnore]
        public string StringExpression
        {
            get
            {
                return Serialize();
            }
            set
            {
                var obj = Deserialize(value);

                Build(obj);
            }
        }

        protected virtual ItemText Deserialize(string exp) => Serializer.Deserialize<ItemText>(exp, RootName);

        protected virtual string Serialize() => Serializer.Serialize(this, RootName);

        protected override void Validate()
        {
            foreach (var item in items)
            {
                if (!IsKeyValid(item.Language))
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

        public void AddOrUpdate(string language, string text)
        {
            AddOrUpdate(new ItemTextUnit() { Language = language, Text = text });
        }

        public void AddOrUpdate(ItemTextUnit item)
        {
            if (items.FirstOrDefault(c => c.Language == item.Language) is ItemTextUnit found)
            {
                found.Text = item.Text;
            }
            else
            {
                items.Add(item);
            }
        }

        public ItemTextUnit FirstOrDefault(string language)
        {
            ItemTextUnit result = null;

            if (items.FirstOrDefault(c => c.Language == language) is ItemTextUnit found)
            {
                result = found;
            }
            else if (items.Count > 0)
            {
                result = items.First();
            }

            return result;
        }

        protected virtual void Build(ItemText source)
        {
            source.items.ForEach(c => AddOrUpdate(c));
        }

        /// <summary>
        /// Заменяет во всех элементах одно строковое значение на другое
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void Replace(string oldValue, string newValue)
        {
            items.ForEach(c => c.Text = c.Text.Replace(oldValue, newValue));
        }

        public override string ToString()
        {
            return (items.Count > 0) ? items.First().Text : base.ToString();
        }
    }
}