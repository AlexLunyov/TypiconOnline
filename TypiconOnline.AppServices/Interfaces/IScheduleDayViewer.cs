using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.AppServices.OutputFiltering;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayViewer<T> where T: class
    {
        T Execute(FilteredOutputDay day);

        T Execute(FilteredOutputWorship viewModel);
    }
}
