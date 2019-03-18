using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public static class YmnosGroupExtensions
    {
        public static ElementViewModelCollection GetViewModel(this YmnosGroup group, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            ElementViewModelCollection viewModel = new ElementViewModelCollection();

            group.Annotation.AppendViewModel(handler, viewModel);
            group.Prosomoion.AppendViewModel(handler, serializer, viewModel, group.Ihos);
            AppendYmnis(group.Ymnis, handler, serializer, viewModel);

            return viewModel;
        }

        public static void AppendViewModel(this ItemText text, IRuleHandler handler, ElementViewModelCollection viewModel)
        {
            if (text?.IsEmpty == false)
            {
                viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Text, 
                    new List<ParagraphViewModel> { ParagraphVMFactory.Create(text, handler.Settings.Language.Name) }));
            }
        }

        public static void AppendViewModel(this Prosomoion prosomoion, IRuleHandler handler,
            IRuleSerializerRoot serializer, ElementViewModelCollection viewModel, int? ihos = null)
        {
            string str = "";

            int typiconId = handler.Settings.TypiconVersionId;
            var language = handler.Settings.Language;

            if (ihos != null)
            {
                //текст "Глас"
                string ihosString = language.IntConverter.ToString((int)ihos);
                str += $"{GetStringValue(typiconId, CommonRuleConstants.IhosText, language.Name, serializer)} {ihosString}. ";
            }

            //самоподобен?
            if (prosomoion?.Self == true)
            {
                str += GetStringValue(typiconId, CommonRuleConstants.SelfText, language.Name, serializer);
            }
            //если подобен
            else if (prosomoion?.IsEmpty == false)
            {
                string p = GetStringValue(typiconId, CommonRuleConstants.ProsomoionText, language.Name, serializer);

                str += $"{p}: \"{ prosomoion.FirstOrDefault(language.Name).Text }\"";
            }

            viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Text,
                    new List<ParagraphViewModel> { ParagraphVMFactory.Create(language.Name, str) }));
        }

        private static void AppendYmnis(List<Ymnos> ymnis, IRuleHandler handler,
            IRuleSerializerRoot serializer, ElementViewModelCollection viewModel)
        {
            var text = GetStringValues(handler, serializer);

            foreach (Ymnos ymnos in ymnis)
            {
                //добавляем стих и песнопение как отдельные объекты
                foreach (ItemText stihos in ymnos.Stihoi)
                {
                    viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Stihos, text.StihosText, 
                        new List<ParagraphViewModel> { ParagraphVMFactory.Create(stihos, handler.Settings.Language.Name) }));
                }

                viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Choir, text.ChoirText,
                    new List<ParagraphViewModel> { ParagraphVMFactory.Create(ymnos.Text, handler.Settings.Language.Name) } ));
            }
        }

        private static (string StihosText, string ChoirText) GetStringValues(IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            //находим Стих и Хор для дальнешей вставки
            string stihos = GetStringValue(handler.Settings.TypiconVersionId, CommonRuleConstants.StihosRule, handler.Settings.Language.Name, serializer);

            string choir = GetStringValue(handler.Settings.TypiconVersionId, CommonRuleConstants.ChoirRule, handler.Settings.Language.Name, serializer);

            return (stihos, choir);
        }

        private static string GetStringValue(int typiconId, string key, string language, IRuleSerializerRoot serializer)
        {
            return serializer.GetCommonRuleStringValue(typiconId, key, language);
        }
    }
}
