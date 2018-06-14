using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class ApostichaVMFactory : YmnosStructureVMFactory
    {
        public ApostichaVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        protected override void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req/*, ElementViewModel viewModel*/)
        {
            InnerAppendCustomForm(req, CommonRuleConstants.ApostichaRule);
        }

        protected virtual void InnerAppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req, string key)
        {
            var header = Serializer.GetCommonRuleFirstChild<TextHolder>(req.Handler.Settings.TypiconId, key);

            req.AppendModelAction(new ElementViewModel() { ViewModelItemFactory.Create(header, req.Handler, Serializer) });
        }
    }
}
