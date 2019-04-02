using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;

namespace TypiconOnline.Domain.ItemTypes
{
    public static class ItemTextExtensions
    {
        /// <summary>
        /// Заменяет для каждого языка старое значение на новое, уникальное для языка
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldValue"></param>
        /// <param name="value"></param>
        public static void ReplaceForEach(this ItemText source, string oldValue, ItemText value)
        {
            if (source == null || value == null || value.IsEmpty)
            {
                return;
            }

            foreach (var item in source.Items)
            {
                var found = value.Items.FirstOrDefault(c => c.Language == item.Language);
                if (found != null)
                {
                    item.Text = item.Text.Replace(oldValue, found.Text);
                }
            }
        }

        /// <summary>
        /// Заменяет для всех языков строковое значение на соответствующее переведенное целочисленное
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldValue"></param>
        /// <param name="integer"></param>
        public static void ReplaceForEach(this ItemText source, string oldValue, int integer)
        {
            foreach (var item in source.Items)
            {
                var newValue = LanguageSettingsFactory.Create(item.Language).IntConverter.ToString(integer);

                item.Text = item.Text.Replace(oldValue, newValue);
            }
        }

        /// <summary>
        /// Соединяет последовательно все значения по каждому языку
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values"></param>
        public static ItemText Merge(this ItemText source, params ItemText[] values)
        {
            return source.Merge(" ", values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="symbol">Строковое значение для разделения элементов</param>
        /// <param name="values"></param>
        public static ItemText Merge(this ItemText source, string symbol, params ItemText[] values)
        {
            if (source == null || values == null || values.Count() == 0)
            {
                return source;
            }

            foreach (var value in values)
            {
                foreach (ItemTextUnit item in value.Items)
                {
                    var found = source.Items.FirstOrDefault(c => c.Language == item.Language);
                    if (found != null)
                    {
                        found.Text += $"{symbol}{item.Text}";
                    }
                    else
                    {
                        source.AddOrUpdate(item.Language, item.Text);
                    }
                }
            }

            return source;
        }

        public static List<ItemText> Clone(this IEnumerable<ItemText> source)
        {
            var result = new List<ItemText>();

            foreach (var item in source)
            {
                result.Add(new ItemText(item));
            }
            return result;
        }

        public static List<ItemTextNoted> Clone(this IEnumerable<ItemTextNoted> source)
        {
            var result = new List<ItemTextNoted>();

            foreach (var item in source)
            {
                result.Add(new ItemTextNoted(item));
            }
            return result;
        }

        /// <summary>
        /// Возвращает первое строковое значение Text.
        /// Используется в локализованных версиях
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetLocal(this ItemText item) => item.Items.FirstOrDefault()?.Text;

    }
}
