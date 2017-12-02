using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    //public class ContainerViewModelFactory : ViewModelFactoryBase<ExecContainer>
    //{
    //    public ContainerViewModelFactory(IRuleSerializerRoot serializer) : base(serializer) { }

    //    public override ElementViewModel Create(ExecContainer container, IRuleHandler handler)
    //    {
    //        ElementViewModel viewModel = new ElementViewModel();

    //        foreach (RuleElement element in container.ChildElements)
    //        {
    //            if ((element is IViewModelElement v)
    //                && (element is ICustomInterpreted c) && handler.IsTypeAuthorized(c))
    //            {
    //                viewModel.AddRange(v.CreateViewModel(handler));
    //            }
    //        }

    //        return viewModel;
    //    }
    //}
}
