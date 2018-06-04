using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.Rules.ViewModels.Factories
{
    public class ParagraphVMFactory
    {
        public static ParagraphViewModel Create(ItemTextNoted itemTextNoted, string language)
        {
            var style = new TextStyle()
            {
                IsBold = itemTextNoted.IsBold,
                IsItalic = itemTextNoted.IsItalic,
                IsRed = itemTextNoted.IsRed,
                Header = itemTextNoted.Header
            };

            var viewModel = new ParagraphViewModel() { Style = style };

            if (itemTextNoted.FirstOrDefault(language) is ItemTextUnit i)
            {
                viewModel.Text = new ItemTextUnit(i);
            }

            if (itemTextNoted.Note != null)
            {
                viewModel.Note = Create(itemTextNoted.Note, language);
            }

            return viewModel;
        }

        public static ParagraphViewModel Create(ItemText itemText, string language)
        {
            var viewModel = new ParagraphViewModel();

            if (itemText?.FirstOrDefault(language) is ItemTextUnit i)
            {
                viewModel.Text = i;
            }

            return viewModel;
        }

        public static List<ParagraphViewModel> CreateList(List<ItemTextNoted> list, string language)
        {
            var viewModel = new List<ParagraphViewModel>();

            list.ForEach(c => viewModel.Add(Create(c, language)));

            return viewModel;
        }

        public static List<ParagraphViewModel> CreateList(List<BookStihos> list, string language)
        {
            var viewModel = new List<ParagraphViewModel>();

            list.ForEach(c => viewModel.Add(Create(c as ItemText, language)));

            return viewModel;
        }

        public static ParagraphViewModel Create(string language, string text)
        {
            return new ParagraphViewModel
            {
                Text = new ItemTextUnit() { Language = language, Text = text }
            };
        }
    }
}
