using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class ViewModelItemFactory
    {
        public static ViewModelItem Create(TextHolderKind kind, List<string> p)
        {
            return Create(kind, string.Empty, p);
        }

        public static ViewModelItem Create(TextHolderKind kind, string kindString, List<string> p)
        {
            return new ViewModelItem()
            {
                Kind = Cast(kind),
                KindStringValue = kindString,
                Paragraphs = p
            };
        }

        public static ViewModelItem Create(ViewModelItemKind kind, List<string> p, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            return new ViewModelItem()
            {
                Kind = kind,
                KindStringValue = GetKindStringValue(kind, handler, serializer),
                Paragraphs = p
            };
        }

        public static ViewModelItem Create(TextHolder textHolder, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            var kind = Cast(textHolder.Kind);
            return new ViewModelItem()
            {
                Kind = kind,
                KindStringValue = GetKindStringValue(kind, handler, serializer),
                Paragraphs = textHolder.Paragraphs.Select(c => c[handler.Settings.Language]).ToList()
            };
        }

        public static ViewModelItem Create(Ymnos ymnos, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            var kind = Cast(ymnos.Kind);
            return new ViewModelItem()
            {
                Kind = Cast(ymnos.Kind),
                KindStringValue = GetKindStringValue(kind, handler, serializer),
                Paragraphs = new List<string>() { ymnos.Text[handler.Settings.Language] }
            };
        }

        private static ViewModelItemKind Cast(YmnosKind kind)
        {
            ViewModelItemKind result = ViewModelItemKind.Text;
            switch (kind)
            {
                case YmnosKind.Irmos:
                    result = ViewModelItemKind.Irmos;
                    break;
                case YmnosKind.Theotokion:
                    result = ViewModelItemKind.Theotokion;
                    break;
                case YmnosKind.Katavasia:
                    //ничего не делаем
                    break;
                default:
                    result = ViewModelItemKind.Troparion;
                    break;
            }
            return result;
        }

        private static ViewModelItemKind Cast(TextHolderKind kind)
        {
            ViewModelItemKind result = ViewModelItemKind.Text;
            switch (kind)
            {
                case TextHolderKind.Choir:
                    result = ViewModelItemKind.Choir;
                    break;
                case TextHolderKind.Deacon:
                    result = ViewModelItemKind.Deacon;
                    break;
                case TextHolderKind.Lector:
                    result = ViewModelItemKind.Lector;
                    break;
                case TextHolderKind.Priest:
                    result = ViewModelItemKind.Priest;
                    break;
                case TextHolderKind.Stihos:
                    result = ViewModelItemKind.Stihos;
                    break;
            }
            return result;
        }

        protected static string GetKindStringValue(ViewModelItemKind kind, IRuleHandler handler, IRuleSerializerRoot ruleSerializer)
        {
            string result = "";

            int index = -1;

            switch (kind)
            {
                case ViewModelItemKind.Choir:
                    index = 0;
                    break;
                case ViewModelItemKind.Deacon:
                    index = 1;
                    break;
                case ViewModelItemKind.Lector:
                    index = 2;
                    break;
                case ViewModelItemKind.Priest:
                    index = 3;
                    break;
                case ViewModelItemKind.Stihos:
                    index = 4;
                    break;
                case ViewModelItemKind.Irmos:
                    index = 5;
                    break;
                case ViewModelItemKind.Troparion:
                    index = 6;
                    break;
                case ViewModelItemKind.Chorus:
                    index = 7;
                    break;
                case ViewModelItemKind.Theotokion:
                    index = 8;
                    break;
                    //default:
                    //    return string.Empty;
            }

            if (index >= 0)
            {
                result = handler.Settings.Rule.Owner.GetCommonRuleIndexedString(
                    new CommonRuleServiceRequest() { Key = CommonRuleConstants.ViewModelKind, RuleSerializer = ruleSerializer }, index, handler.Settings.Language);
            }

            return result;
        }
    }
}
