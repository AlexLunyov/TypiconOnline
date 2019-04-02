using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Rules.Output.Factories
{
    public class ViewModelItemFactory
    {
        public static OutputSection Create(TextHolderKind kind, List<ItemTextNoted> p)
        {
            return Create(kind, default(ItemText), p);
        }

        public static OutputSection Create(TextHolderKind kind, ItemText kindText, List<ItemTextNoted> p)
        {
            return new OutputSection()
            {
                Kind = Cast(kind),
                KindText = kindText,
                Paragraphs = p
            };
        }

        public static OutputSection Create(ElementViewModelKind kind, List<ItemTextNoted> p, int typiconVersionId, IRuleSerializerRoot serializer)
        {
            return new OutputSection()
            {
                Kind = kind,
                KindText = GetKindItemTextValue(kind, typiconVersionId, serializer),
                Paragraphs = p
            };
        }

        public static OutputSection Create(TextHolder textHolder, int typiconVersionId, IRuleSerializerRoot serializer)
        {
            var kind = Cast(textHolder.Kind);
            return new OutputSection()
            {
                Kind = kind,
                KindText = GetKindItemTextValue(kind, typiconVersionId, serializer),
                Paragraphs = textHolder.Paragraphs
            };
        }

        public static OutputSection Create(Ymnos ymnos, int typiconVersionId, IRuleSerializerRoot serializer)
        {
            var kind = Cast(ymnos.Kind);
            return new OutputSection()
            {
                Kind = Cast(ymnos.Kind),
                KindText = GetKindItemTextValue(kind, typiconVersionId, serializer),
                Paragraphs = new List<ItemTextNoted>() { new ItemTextNoted(ymnos.Text) }
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

        protected static ItemText GetKindItemTextValue(ElementViewModelKind kind, int typiconVersionId, IRuleSerializerRoot ruleSerializer)
        {
            ItemText result = null;

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
                result = ruleSerializer.GetCommonRuleIndexedItemText(typiconVersionId, CommonRuleConstants.ViewModelKind, index);
            }

            return result;
        }
    }
}
