using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
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
