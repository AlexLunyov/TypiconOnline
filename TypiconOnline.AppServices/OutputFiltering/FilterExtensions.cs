using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Typicon.Output;
//using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.Domain.WebQuery.OutputFiltering
{
    public static class FilterExtensions
    {
        /// <summary>
        /// Оставляем только элемент с указанным языком, либо любой первый, если указанного нет
        /// </summary>
        /// <param name="filter.Language"></param>
        public static ItemTextUnit FilterOut(this ItemText item, OutputFilter filter)
        {
            var found = item.FirstOrDefault(filter.Language);

            return (found != null) ? new ItemTextUnit(found.Language, found.Text) : default(ItemTextUnit);
        }

        public static FilteredParagraph FilterOut(this ItemTextStyled item, OutputFilter filter)
        {
            return new FilteredParagraph
            {
                Text = (item as ItemText).FilterOut(filter),
                Style = new TextStyle()
                {
                    IsBold = item.IsBold,
                    IsItalic = item.IsItalic,
                    IsRed = item.IsRed
                }
            };
        }


        public static FilteredParagraphNoted FilterOut(this ItemTextNoted item, OutputFilter filter)
        {
            return new FilteredParagraphNoted
            {
                Text = (item as ItemText).FilterOut(filter),
                Style = new TextStyle()
                {
                    Header = item.Header,
                    IsBold = item.IsBold,
                    IsItalic = item.IsItalic,
                    IsRed = item.IsRed
                },
                Note = item.Note?.FilterOut(filter)
            };
        }

        public static FilteredOutputSection FilterOut(this OutputSectionModel section, OutputFilter filter)
            => new FilteredOutputSection()
            {
                KindText = section.KindText?.FilterOut(filter),
                Kind = section.Kind,
                Paragraphs = section.Paragraphs.FilterOut(filter)
            };


        public static List<FilteredOutputSection> FilterOut(this List<OutputSectionModel> collection, OutputFilter filter)
        {
            var result = new List<FilteredOutputSection>();
            collection.ForEach(c => result.Add(c.FilterOut(filter)));
            return result;
        }

        public static List<FilteredParagraphNoted> FilterOut(this List<ItemTextNoted> collection, OutputFilter filter)
        {
            var result = new List<FilteredParagraphNoted>();
            collection.ForEach(c => result.Add(c.FilterOut(filter)));
            return result;
        }

        public static FilteredOutputWorship FilterOut(this OutputWorship worship, OutputFilter filter)
            => new FilteredOutputWorship
            {
                Id = worship.Id,
                Time = worship.Time,
                Name = worship.Name?.FilterOut(filter),
                AdditionalName = worship.AdditionalName?.FilterOut(filter),
                HasSequence = !string.IsNullOrEmpty(worship.Definition) 
            };

        public static List<FilteredOutputWorship> FilterOut(this List<OutputWorship> collection, OutputFilter filter)
        {
            var result = new List<FilteredOutputWorship>();
            collection.ForEach(c => result.Add(c.FilterOut(filter)));
            return result;
        }

        public static List<FilteredOutputDay> FilterOut(this List<OutputDay> collection, OutputFilter filter)
        {
            var result = new List<FilteredOutputDay>();
            collection.ForEach(c => result.Add(c.FilterOut(filter)));
            return result;
        }

        public static FilteredOutputDay FilterOut(this OutputDay day, OutputFilter filter)
         => new FilteredOutputDay()
            {
                Name = day.Name.FilterOut(filter),
                Date = day.Date,
                SignName = day.PredefinedSign.SignName.FilterOut(filter),
                SignNumber = day.PrintDayTemplate.Number,
                Icon = day.PrintDayTemplate.Icon,
                IsRed = day.PrintDayTemplate.IsRed,
                Worships = day.Worships
                            .OrderBy(c => c.Order)
                            .ToList()
                            .FilterOut(filter)
         };
    }
}
