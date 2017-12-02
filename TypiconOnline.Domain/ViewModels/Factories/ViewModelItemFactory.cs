using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class ViewModelItemFactory
    {
        public static ViewModelItem Create(TextHolderKind kind, IEnumerable<string> p)
        {
            return Create(kind, string.Empty, p);
        }

        public static ViewModelItem Create(TextHolderKind kind, string kindString, IEnumerable<string> p)
        {
            return new ViewModelItem()
            {
                Kind = kind,
                KindStringValue = kindString,
                Paragraphs = p
            };
        }

        public static ViewModelItem Create(TextHolder textHolder, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            return new ViewModelItem()
            {
                Kind = textHolder.Kind,
                KindStringValue = GetKindStringValue(textHolder.Kind, handler, serializer),
                Paragraphs = textHolder.Paragraphs.Select(c => c[handler.Settings.Language])
            };
        }

        protected static string GetKindStringValue(TextHolderKind kind, IRuleHandler handler, IRuleSerializerRoot ruleSerializer)
        {
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = ruleSerializer };

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
