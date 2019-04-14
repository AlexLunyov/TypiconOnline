using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Output.Factories
{
    public class ExapostilarionRuleVMFactory : ViewModelFactoryBase<ExapostilarionRule>
    {
        public ExapostilarionRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer)
        {
        }

        public override void Create(CreateViewModelRequest<ExapostilarionRule> req)
        {
            if (req.Element == null
                || req.Element.Structure == null)
            {
                //TODO: просто ничего не делаем, хотя надо бы это обрабатывать
                return;
            }


        }
    }
}
