using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class LocalizeExtensions
    {
        /// <summary>
        /// Оставляем только элемент с указанным языком, либо любой первый, если указанного нет
        /// </summary>
        /// <param name="language"></param>
        public static ItemTextUnit Localize(this ItemText item, string language)
        {
            var found = item.FirstOrDefault(language);

            return (found != null) ? new ItemTextUnit(found.Language, found.Text) : default(ItemTextUnit);
        }

        public static LocalizedParagraph Localize(this ItemTextStyled item, string language)
        {
            return new LocalizedParagraph
            {
                Text = (item as ItemText).Localize(language),
                Style = new TextStyle()
                {
                    IsBold = item.IsBold,
                    IsItalic = item.IsItalic,
                    IsRed = item.IsRed
                }
            };
        }


        public static LocalizedParagraphNoted Localize(this ItemTextNoted item, string language)
        {
            return new LocalizedParagraphNoted
            {
                Text = (item as ItemText).Localize(language),
                Style = new TextStyle()
                {
                    Header = item.Header,
                    IsBold = item.IsBold,
                    IsItalic = item.IsItalic,
                    IsRed = item.IsRed
                },
                Note = item.Note?.Localize(language)
            };
        }

        public static List<LocalizedOutputSection> Localize(this List<OutputSection> collection, string language)
        {
            var result = new List<LocalizedOutputSection>();
            collection.ForEach(c => result.Add(c.Localize(language)));
            return result;
        }

        public static List<LocalizedParagraphNoted> Localize(this List<ItemTextNoted> collection, string language)
        {
            var result = new List<LocalizedParagraphNoted>();
            collection.ForEach(c => result.Add(c.Localize(language)));
            return result;
        }

        public static List<LocalizedOutputWorship> Localize(this List<OutputWorship> collection, string language)
        {
            var result = new List<LocalizedOutputWorship>();
            collection.ForEach(c => result.Add(c.Localize(language)));
            return result;
        }

        public static List<LocalizedOutputDay> Localize(this List<OutputDay> collection, string language)
        {
            var result = new List<LocalizedOutputDay>();
            collection.ForEach(c => result.Add(c.Localize(language)));
            return result;
        }
    }
}
