using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class ParagraphVMFactory
    {
        public static ParagraphViewModel Create(ItemTextNoted itemTextNoted, string language)
        {
            var viewModel = new ParagraphViewModel
            {
                Style = itemTextNoted.Style
            };

            if (itemTextNoted[language] is string s)
            {
                viewModel.Text = s;
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

            if (itemText?[language] is string s)
            {
                viewModel.Text = s;
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

        public static ParagraphViewModel Create(string str)
        {
            return new ParagraphViewModel
            {
                Text = str
            };
        }
    }
}
