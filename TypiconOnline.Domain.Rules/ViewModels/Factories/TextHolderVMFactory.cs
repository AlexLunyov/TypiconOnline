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
using TypiconOnline.Domain.Rules.ViewModels.Factories;
using TypiconOnline.Domain.Rules.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.ViewModels.Factories
{
    public class TextHolderVMFactory : ViewModelFactoryBase<TextHolder>
    {
        public TextHolderVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        public override void Create(CreateViewModelRequest<TextHolder> req)
        {
            if (req.Element == null)
            {
                //TODO: просто ничего не делаем, хотя надо бы это обрабатывать
                return;
            }

            ViewModelItem item = ViewModelItemFactory.Create(req.Element, req.Handler, Serializer);

            req.AppendModelAction(new ElementViewModel() { item });
        }


    }
}
