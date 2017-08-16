using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.ViewModels
{
    public class ContainerViewModel : ElementViewModel
    {
        public List<ElementViewModel> ChildElements = new List<ElementViewModel>();
    }
}
