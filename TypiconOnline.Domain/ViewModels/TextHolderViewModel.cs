using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    public class TextHolderViewModel : ElementViewModel
    {
        public string KindStringValue { get; set; }
        public TextHolderKind Kind { get; set; }
        public IEnumerable<string> Paragraphs { get; set; }
        public IRuleSerializerRoot Serializer { get; }

        public TextHolderViewModel(IRuleSerializerRoot serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
            Paragraphs = new List<string>();
        }

        public TextHolderViewModel(TextHolder textHolder, IRuleHandler handler)
        {
            if (textHolder == null || textHolder.Serializer == null) throw new ArgumentNullException("textHolder");
            if (handler == null) throw new ArgumentNullException("handler");

            Serializer = textHolder.Serializer;

            //textHolder.ThrowExceptionIfInvalid();

            Kind = textHolder.Kind;
            KindStringValue = GetKindStringValue(textHolder.Kind, handler);

            Paragraphs = textHolder.Paragraphs.Select(c => c[handler.Settings.Language]);
        }

        private string GetKindStringValue(TextHolderKind kind, IRuleHandler handler)
        {
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = Serializer };

            switch (kind)
            {
                case TextHolderKind.Choir:
                    req.Key = CommonRuleConstants.ChoirRule;
                    break;
                case TextHolderKind.Deacon:
                    req.Key = CommonRuleConstants.DeaconRule;
                    break;
                case TextHolderKind.Lector:
                    req.Key = CommonRuleConstants.LectorRule;
                    break;
                case TextHolderKind.Priest:
                    req.Key = CommonRuleConstants.PriestRule;
                    break;
                case TextHolderKind.Stihos:
                    req.Key = CommonRuleConstants.StihosRule;
                    break;
                default:
                    return string.Empty;
            }

            return handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);
        }
    }
}
