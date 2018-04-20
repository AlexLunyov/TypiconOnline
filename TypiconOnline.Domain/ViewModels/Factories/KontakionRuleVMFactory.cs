using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class KontakionRuleVMFactory : ViewModelFactoryBase<KontakionRule>
    {
        private const string IHOS_STRING = "[ihos]";

        public KontakionRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        public override void Create(CreateViewModelRequest<KontakionRule> req)
        {
            if (req.Element?.Calculate(req.Handler.Settings) is YmnosStructure kontakion
                && kontakion.Groups.Count > 0)
            {
                var headers = GetHeaders(req, kontakion);

                AppendKontakion(req, kontakion, headers.Kontakion);

                if (req.Element.ShowIkos && kontakion.Groups[0].Ymnis.Count > 1)
                {
                    AppendIkos(req, kontakion.Groups[0].Ymnis[1].Text, headers.Ikos);
                }
            }
        }

        private void AppendKontakion(CreateViewModelRequest<KontakionRule> req, YmnosStructure kontakion, ViewModelItem view)
        {
            var viewModel = new ElementViewModel() { view };

            var group = kontakion.Groups[0];

            group.Annotation.AppendViewModel(req.Handler, viewModel);
            group.Prosomoion.AppendViewModel(req.Handler, Serializer, viewModel);
            group.Ymnis[0].Text.AppendViewModel(req.Handler, viewModel);

            req.AppendModelAction(viewModel);
        }

        private void AppendIkos(CreateViewModelRequest<KontakionRule> req, ItemText ikos, ViewModelItem view)
        {
            var viewModel = new ElementViewModel() { view };

            ikos.AppendViewModel(req.Handler, viewModel);

            req.AppendModelAction(viewModel);
        }

        private (ViewModelItem Kontakion, ViewModelItem Ikos) GetHeaders(CreateViewModelRequest<KontakionRule> req, YmnosStructure kontakion)
        {
            List<TextHolder> headers = req.Handler.Settings.TypiconRule.Owner.GetCommonRuleChildren(
                    new CommonRuleServiceRequest() { Key = CommonRuleConstants.Kontakion, RuleSerializer = Serializer }).Cast<TextHolder>().ToList();

            var viewKontakion = ViewModelItemFactory.Create(headers[0], req.Handler, Serializer);

            viewKontakion.Paragraphs[0].Replace(IHOS_STRING,
                req.Handler.Settings.Language.IntConverter.ToString(kontakion.Ihos));

            var viewIkos = ViewModelItemFactory.Create(headers[1], req.Handler, Serializer);

            return (viewKontakion, viewIkos);
        }
    }
}
