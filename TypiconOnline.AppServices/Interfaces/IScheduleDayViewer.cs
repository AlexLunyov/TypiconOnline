using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleDayViewer<T> where T: class
    {
        T Execute(LocalizedOutputDay day);

        T Execute(LocalizedOutputWorship viewModel);
    }
}
