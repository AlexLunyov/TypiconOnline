using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Output.Factories
{
    public class KontakionRuleVMFactory : ViewModelFactoryBase<KontakionRule>
    {
        private const string IHOS_STRING = "[ihos]";

        public KontakionRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        public override void Create(CreateViewModelRequest<KontakionRule> req)
        {
            if (req.Element?.Calculate(req.Handler.Settings) is Kontakion kontakion)
            {
                var headers = GetHeaders(req, kontakion);

                AppendKontakion(req, kontakion, headers.Kontakion);

                if (req.Element.ShowIkos && kontakion.Ikos != null)
                {
                    AppendIkos(req, kontakion.Ikos, headers.Ikos);
                }
            }
        }

        private void AppendKontakion(CreateViewModelRequest<KontakionRule> req, Kontakion kontakion, OutputSection view)
        {
            var viewModel = new OutputElementCollection() { view };

            kontakion.Annotation.AppendViewModel(req.Handler, viewModel);
            kontakion.Prosomoion.AppendViewModel(req.Handler.Settings.TypiconVersionId, Serializer, viewModel);
            kontakion.Ymnos.AppendViewModel(req.Handler, viewModel);

            req.AppendModelAction(viewModel);
        }

        private void AppendIkos(CreateViewModelRequest<KontakionRule> req, ItemText ikos, OutputSection view)
        {
            var viewModel = new OutputElementCollection() { view };

            ikos.AppendViewModel(req.Handler, viewModel);

            req.AppendModelAction(viewModel);
        }

        private (OutputSection Kontakion, OutputSection Ikos) GetHeaders(CreateViewModelRequest<KontakionRule> req, Kontakion kontakion)
        {
            var headers = Serializer.GetCommonRuleChildren<TextHolder>(req.Handler.Settings.TypiconVersionId, CommonRuleConstants.Kontakion);

            var viewKontakion = ViewModelItemFactory.Create(headers[0], req.Handler.Settings.TypiconVersionId, Serializer);

            viewKontakion.Paragraphs[0].ReplaceForEach(IHOS_STRING, kontakion.Ihos);

            var viewIkos = ViewModelItemFactory.Create(headers[1], req.Handler.Settings.TypiconVersionId, Serializer);

            return (viewKontakion, viewIkos);
        }
    }
}
