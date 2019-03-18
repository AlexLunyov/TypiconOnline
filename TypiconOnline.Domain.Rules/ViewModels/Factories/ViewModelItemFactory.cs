using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class ViewModelItemFactory
    {
        public static ElementViewModel Create(TextHolderKind kind, List<ParagraphViewModel> p)
        {
            return Create(kind, string.Empty, p);
        }

        public static ElementViewModel Create(TextHolderKind kind, string kindString, List<ParagraphViewModel> p)
        {
            return new ElementViewModel()
            {
                Kind = Cast(kind),
                KindValue = kindString,
                Paragraphs = p
            };
        }

        public static ElementViewModel Create(ElementViewModelKind kind, List<ParagraphViewModel> p, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            return new ElementViewModel()
            {
                Kind = kind,
                KindValue = GetKindStringValue(kind, handler, serializer),
                Paragraphs = p
            };
        }

        public static ElementViewModel Create(TextHolder textHolder, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            var kind = Cast(textHolder.Kind);
            return new ElementViewModel()
            {
                Kind = kind,
                KindValue = GetKindStringValue(kind, handler, serializer),
                Paragraphs = ParagraphVMFactory.CreateList(textHolder.Paragraphs, handler.Settings.Language.Name)
            };
        }

        public static ElementViewModel Create(Ymnos ymnos, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            var kind = Cast(ymnos.Kind);
            return new ElementViewModel()
            {
                Kind = Cast(ymnos.Kind),
                KindValue = GetKindStringValue(kind, handler, serializer),
                Paragraphs = new List<ParagraphViewModel>() { ParagraphVMFactory.Create(ymnos.Text, handler.Settings.Language.Name) }
            };
        }

        private static ElementViewModelKind Cast(YmnosKind kind)
        {
            ElementViewModelKind result = ElementViewModelKind.Text;
            switch (kind)
            {
                case YmnosKind.Irmos:
                    result = ElementViewModelKind.Irmos;
                    break;
                case YmnosKind.Theotokion:
                    result = ElementViewModelKind.Theotokion;
                    break;
                case YmnosKind.Katavasia:
                    //ничего не делаем
                    break;
                default:
                    result = ElementViewModelKind.Troparion;
                    break;
            }
            return result;
        }

        private static ElementViewModelKind Cast(TextHolderKind kind)
        {
            ElementViewModelKind result = ElementViewModelKind.Text;
            switch (kind)
            {
                case TextHolderKind.Choir:
                    result = ElementViewModelKind.Choir;
                    break;
                case TextHolderKind.Deacon:
                    result = ElementViewModelKind.Deacon;
                    break;
                case TextHolderKind.Lector:
                    result = ElementViewModelKind.Lector;
                    break;
                case TextHolderKind.Priest:
                    result = ElementViewModelKind.Priest;
                    break;
                case TextHolderKind.Stihos:
                    result = ElementViewModelKind.Stihos;
                    break;
            }
            return result;
        }

        protected static string GetKindStringValue(ElementViewModelKind kind, IRuleHandler handler, IRuleSerializerRoot ruleSerializer)
        {
            string result = "";

            int index = -1;

            switch (kind)
            {
                case ElementViewModelKind.Choir:
                    index = 0;
                    break;
                case ElementViewModelKind.Deacon:
                    index = 1;
                    break;
                case ElementViewModelKind.Lector:
                    index = 2;
                    break;
                case ElementViewModelKind.Priest:
                    index = 3;
                    break;
                case ElementViewModelKind.Stihos:
                    index = 4;
                    break;
                case ElementViewModelKind.Irmos:
                    index = 5;
                    break;
                case ElementViewModelKind.Troparion:
                    index = 6;
                    break;
                case ElementViewModelKind.Chorus:
                    index = 7;
                    break;
                case ElementViewModelKind.Theotokion:
                    index = 8;
                    break;
                    //default:
                    //    return string.Empty;
            }

            if (index >= 0)
            {
                result = ruleSerializer.GetCommonRuleIndexedString(handler.Settings.TypiconVersionId, CommonRuleConstants.ViewModelKind, 
                    handler.Settings.Language.Name, index);
            }

            return result;
        }
    }
}
