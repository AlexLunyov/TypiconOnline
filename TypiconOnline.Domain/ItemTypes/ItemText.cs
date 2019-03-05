﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.ItemTypes
{
    [Serializable]
    public class ItemText : ItemType, IEquatable<ItemText>
    {
        public ItemText() { }

        public ItemText(ItemText source) : this()
        {
            if (source == null) throw new ArgumentNullException("source in ItemText");

            Build(source);
        }

        private List<ItemTextUnit> _items;

        [XmlElement("item")]
        public virtual List<ItemTextUnit> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<ItemTextUnit>();
                }
                return _items;
            }

            set
            {
                _items = value;
            }
        }

        public virtual bool IsEmpty => Items.Count == 0;

        public IEnumerable<string> Languages => Items.Select(c => c.Language);

        protected override void Validate()
        {
            foreach (var item in Items)
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
            if (Items.FirstOrDefault(c => c.Language == item.Language) is ItemTextUnit found)
            {
                found.Text = item.Text;
            }
            else
            {
                Items.Add(item);
            }
        }

        /// <summary>
        /// Возвращает текст согласно введенному языку, либо первый попавшийся
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public ItemTextUnit FirstOrDefault(string language)
        {
            ItemTextUnit result = null;

            if (Items.FirstOrDefault(c => c.Language == language) is ItemTextUnit found)
            {
                result = found;
            }
            else if (Items.Count > 0)
            {
                result = Items.First();
            }

            //Возвращаем новый объект, чтобы избежать ошибки при сериализации
            return (result != null ) ? new ItemTextUnit(result.Language, result.Text) : default(ItemTextUnit);
        }

        protected virtual void Build(ItemText source)
        {
            source.Items.ForEach(AddOrUpdate);
        }

        /// <summary>
        /// Применяет метод string.Replace ко всем строковым значениям элемента
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void Replace(string oldValue, string newValue)
        {
            Items.ForEach(c => c.Text = c.Text.Replace(oldValue, newValue));
        }

        public override string ToString()
        {
            return (Items.Count > 0) ? Items.First().Text : base.ToString();
        }

        /// <summary>
        /// Сравниваем только элементы Items
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ItemText other)
        {
            bool result = false;

            if (other?.Items.Count == Items.Count)
            {
                result = true;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Language != other.Items[i].Language
                        || Items[i].Text != other.Items[i].Text)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemText);
        }
    }
}