using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Output.Factories
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

            OutputSection item = ViewModelItemFactory.Create(req.Element, req.Handler.Settings.TypiconVersionId, Serializer);

            req.AppendModelAction(new OutputElementCollection() { item });
        }


    }
}
