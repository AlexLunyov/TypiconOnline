using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels.Factories;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class TextHolderVMFactory : ViewModelFactoryBase<TextHolder>
    {
        public TextHolderVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        public override void Create(CreateViewModelRequest<TextHolder> req)
        {
            ViewModelItem item = ViewModelItemFactory.Create(req.Element, req.Handler, Serializer);

            req.AppendModelAction(new ElementViewModel() { item });
        }


    }
}
