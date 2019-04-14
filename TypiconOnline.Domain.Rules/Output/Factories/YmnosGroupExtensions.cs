using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Output.Factories
{
    public static class YmnosGroupExtensions
    {
        public static OutputElementCollection GetViewModel(this YmnosGroup group, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            OutputElementCollection viewModel = new OutputElementCollection();

            group.Annotation.AppendViewModel(handler, viewModel);
            group.Prosomoion.AppendViewModel(handler.Settings.TypiconVersionId, serializer, viewModel, group.Ihos);
            AppendYmnis(group.Ymnis, handler, serializer, viewModel);

            return viewModel;
        }

        public static void AppendViewModel(this ItemText text, IRuleHandler handler, OutputElementCollection viewModel)
        {
            if (text?.IsEmpty == false)
            {
                viewModel.Add(OutputSectionFactory.Create(TextHolderKind.Text, 
                    new List<ItemTextNoted> { new ItemTextNoted(text) }));
            }
        }

        public static void AppendViewModel(this Prosomoion prosomoion, int typiconVersionId,
            IRuleSerializerRoot serializer, OutputElementCollection viewModel, int? ihos = null)
        {
            ItemText ihosItemText = null;
            ItemText prosomoionItemText = null;

            if (ihos != null)
            {
                //текст "Глас"
                ihosItemText = serializer.GetCommonRuleItemTextValue(typiconVersionId, CommonRuleConstants.IhosText);
                ihosItemText.ReplaceForEach("[ihos]", ihos.Value);
            }
            else
            {
                ihosItemText = new ItemText();
            }

            //самоподобен?
            if (prosomoion?.Self == true)
            {
                prosomoionItemText = serializer.GetCommonRuleItemTextValue(typiconVersionId, CommonRuleConstants.SelfText);
            }
            //если подобен
            else if (prosomoion?.IsEmpty == false)
            {
                prosomoionItemText = serializer.GetCommonRuleItemTextValue(typiconVersionId, CommonRuleConstants.ProsomoionText);
                prosomoionItemText.ReplaceForEach("[name]", prosomoion);
            }
            else
            {
                prosomoionItemText = new ItemText();
            }

            //соединяем воедино
            ihosItemText.Merge(prosomoionItemText);

            viewModel.Add(OutputSectionFactory.Create(TextHolderKind.Text,
                    new List<ItemTextNoted> { new ItemTextNoted(ihosItemText) }));
        }

        private static void AppendYmnis(List<Ymnos> ymnis, IRuleHandler handler,
            IRuleSerializerRoot serializer, OutputElementCollection viewModel)
        {
            var (StihosText, ChoirText) = GetStringValues(handler, serializer);

            foreach (Ymnos ymnos in ymnis)
            {
                //добавляем стих и песнопение как отдельные объекты
                foreach (ItemText stihos in ymnos.Stihoi)
                {
                    viewModel.Add(OutputSectionFactory.Create(TextHolderKind.Stihos, StihosText, 
                        new List<ItemTextNoted> { new ItemTextNoted(stihos) }));
                }

                viewModel.Add(OutputSectionFactory.Create(TextHolderKind.Choir, ChoirText,
                    new List<ItemTextNoted> { new ItemTextNoted(ymnos.Text) } ));
            }
        }

        private static (ItemText StihosText, ItemText ChoirText) GetStringValues(IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            //находим Стих и Хор для дальнешей вставки
            var stihos = serializer.GetCommonRuleItemTextValue(handler.Settings.TypiconVersionId, CommonRuleConstants.StihosRule);

            var choir = serializer.GetCommonRuleItemTextValue(handler.Settings.TypiconVersionId, CommonRuleConstants.ChoirRule);

            return (stihos, choir);
        }

        private static string GetStringValue(int typiconId, string key, string language, IRuleSerializerRoot serializer)
        {
            return serializer.GetCommonRuleStringValue(typiconId, key, language);
        }
    }
}
