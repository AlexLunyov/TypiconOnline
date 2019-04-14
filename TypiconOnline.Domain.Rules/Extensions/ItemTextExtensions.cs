using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Extensions
{
    public static class ItemTextExtensions
    {
        public static List<ItemTextNoted> Clone(this List<ItemTextNoted> list)
        {
            var viewModel = new List<ItemTextNoted>();

            list.ForEach(c => viewModel.Add(new ItemTextNoted(c)));

            return viewModel;
        }
    }
}
