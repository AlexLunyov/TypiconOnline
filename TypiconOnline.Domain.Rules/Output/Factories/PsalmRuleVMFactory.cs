using System.Linq;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Output.Messaging;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;
using System.Collections.Generic;

namespace TypiconOnline.Domain.Rules.Output.Factories
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
            TextHolder header = Serializer.GetCommonRuleFirstChild<TextHolder>(req.Handler.Settings.TypiconVersionId, CommonRuleConstants.Psalm);

            var viewHeader = ViewModelItemFactory.Create(header, req.Handler.Settings.TypiconVersionId, Serializer);

            //вставляем номер Псалма
            viewHeader.Paragraphs[0].ReplaceForEach(NUMBER, req.Element.Number);

            req.AppendModelAction(new OutputElementCollection() { viewHeader });
        }

        private void AppendText(CreateViewModelRequest<PsalmRule> req, BookReading psalmReading)
        {
            //List<string> paragraphs = psalmReading.Text.Select(c => c[req.Handler.Settings.Language.Name]).ToList();
            var paragraphs = new List<ItemTextNoted>();
            psalmReading.Text.ForEach(c => paragraphs.Add(new ItemTextNoted(c)));

            req.AppendModelAction(new OutputElementCollection() { ViewModelItemFactory.Create(TextHolderKind.Lector, paragraphs) });
        }
    }
}
