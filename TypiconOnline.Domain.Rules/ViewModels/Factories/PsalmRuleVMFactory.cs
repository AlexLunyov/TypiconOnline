using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ViewModels.Messaging;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class PsalmRuleVMFactory : ViewModelFactoryBase<PsalmRule>
    {
        private const string NUMBER = "[number]";

        public PsalmRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer)
        {
        }

        public override void Create(CreateViewModelRequest<PsalmRule> req)
        {
            if (req.Element?.Calculate(req.Handler.Settings) is BookReading psalmReading)
            {
                AppendHeader(req);
                AppendText(req, psalmReading);
            }
        }

        private void AppendHeader(CreateViewModelRequest<PsalmRule> req)
        {
            TextHolder header = Serializer.GetCommonRuleFirstChild<TextHolder>(req.Handler.Settings.TypiconId, CommonRuleConstants.Psalm);

            var viewHeader = ViewModelItemFactory.Create(header, req.Handler, Serializer);

            //вставляем номер Псалма
            viewHeader.Paragraphs[0].Replace(NUMBER, req.Handler.Settings.Language.IntConverter.ToString(req.Element.Number));

            req.AppendModelAction(new ElementViewModelCollection() { viewHeader });
        }

        private void AppendText(CreateViewModelRequest<PsalmRule> req, BookReading psalmReading)
        {
            //List<string> paragraphs = psalmReading.Text.Select(c => c[req.Handler.Settings.Language.Name]).ToList();
            var paragraphs = ParagraphVMFactory.CreateList(psalmReading.Text, req.Handler.Settings.Language.Name);

            req.AppendModelAction(new ElementViewModelCollection() { ViewModelItemFactory.Create(TextHolderKind.Lector, paragraphs) });
        }
    }
}
