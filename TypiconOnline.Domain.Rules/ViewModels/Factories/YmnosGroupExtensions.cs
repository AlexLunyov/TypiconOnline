using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Rules.ViewModels.Factories
{
    public static class YmnosGroupExtensions
    {
        public static ElementViewModel GetViewModel(this YmnosGroup group, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            ElementViewModel viewModel = new ElementViewModel();

            group.Annotation.AppendViewModel(handler, viewModel);
            group.Prosomoion.AppendViewModel(handler, serializer, viewModel, group.Ihos);
            AppendYmnis(group.Ymnis, handler, serializer, viewModel);

            return viewModel;
        }

        public static void AppendViewModel(this ItemText text, IRuleHandler handler, ElementViewModel viewModel)
        {
            if (text?.IsEmpty == false)
            {
                viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Text, 
                    new List<ParagraphViewModel> { ParagraphVMFactory.Create(text, handler.Settings.Language.Name) }));
            }
        }

        public static void AppendViewModel(this Prosomoion prosomoion, IRuleHandler handler,
            IRuleSerializerRoot serializer, ElementViewModel viewModel, int? ihos = null)
        {
            TypiconEntity typ = handler.Settings.TypiconRule.Owner;
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = serializer };

            string str = "";

            var language = handler.Settings.Language;

            if (ihos != null)
            {
                //текст "Глас"
                req.Key = CommonRuleConstants.IhosText;
                string ihosString = language.IntConverter.ToString((int)ihos);
                str += $"{typ.GetTextValue(req, language.Name)} {ihosString}. ";
            }

            //самоподобен?
            if (prosomoion?.Self == true)
            {
                req.Key = CommonRuleConstants.SelfText;
                str += typ.GetTextValue(req, language.Name);
            }
            //если подобен
            else if (prosomoion?.IsEmpty == false)
            {
                req.Key = CommonRuleConstants.ProsomoionText;
                string p = typ.GetTextValue(req, language.Name);

                str += $"{p}: \"{ prosomoion.FirstOrDefault(language.Name).Text }\"";
            }

            viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Text,
                    new List<ParagraphViewModel> { ParagraphVMFactory.Create(language.Name, str) }));
        }

        private static void AppendYmnis(List<Ymnos> ymnis, IRuleHandler handler,
            IRuleSerializerRoot serializer, ElementViewModel viewModel)
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
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = serializer };

            //находим Стих и Хор для дальнешей вставки
            req.Key = CommonRuleConstants.StihosRule;
            string stihos = handler.Settings.TypiconRule.Owner.GetTextValue(req, handler.Settings.Language.Name);

            req.Key = CommonRuleConstants.ChoirRule;
            string choir = handler.Settings.TypiconRule.Owner.GetTextValue(req, handler.Settings.Language.Name);

            return (stihos, choir);
        }
    }
}
