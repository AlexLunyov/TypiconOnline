using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
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

        private void AppendKontakion(CreateViewModelRequest<KontakionRule> req, Kontakion kontakion, ElementViewModel view)
        {
            var viewModel = new ElementViewModelCollection() { view };

            kontakion.Annotation.AppendViewModel(req.Handler, viewModel);
            kontakion.Prosomoion.AppendViewModel(req.Handler, Serializer, viewModel);
            kontakion.Ymnos.AppendViewModel(req.Handler, viewModel);

            req.AppendModelAction(viewModel);
        }

        private void AppendIkos(CreateViewModelRequest<KontakionRule> req, ItemText ikos, ElementViewModel view)
        {
            var viewModel = new ElementViewModelCollection() { view };

            ikos.AppendViewModel(req.Handler, viewModel);

            req.AppendModelAction(viewModel);
        }

        private (ElementViewModel Kontakion, ElementViewModel Ikos) GetHeaders(CreateViewModelRequest<KontakionRule> req, Kontakion kontakion)
        {
            var headers = Serializer.GetCommonRuleChildren<TextHolder>(req.Handler.Settings.TypiconVersionId, CommonRuleConstants.Kontakion);

            var viewKontakion = ViewModelItemFactory.Create(headers[0], req.Handler, Serializer);

            viewKontakion.Paragraphs[0].Replace(IHOS_STRING,
                req.Handler.Settings.Language.IntConverter.ToString(kontakion.Ihos));

            var viewIkos = ViewModelItemFactory.Create(headers[1], req.Handler, Serializer);

            return (viewKontakion, viewIkos);
        }
    }
}
