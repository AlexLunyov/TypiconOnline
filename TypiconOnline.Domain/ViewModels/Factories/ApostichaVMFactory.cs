using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
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
            TextHolder header = Serializer.QueryProcessor.
                Process(new CommonRuleChildrenQuery(req.Handler.Settings.TypiconId, key, Serializer))
                .FirstOrDefault() as TextHolder;

            req.AppendModelAction(new ElementViewModel() { ViewModelItemFactory.Create(header, req.Handler, Serializer) });
        }
    }
}
