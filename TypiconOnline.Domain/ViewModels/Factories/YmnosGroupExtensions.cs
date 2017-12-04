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

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public static class YmnosGroupExtensions
    {
        public static ElementViewModel GetViewModel(this YmnosGroup group, IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            ElementViewModel viewModel = new ElementViewModel();

            AppendAnnotation(group.Annotation, handler, viewModel);
            AppendIhos(group.Ihos, group.Prosomoion, handler, serializer, viewModel);
            AppendYmnis(group.Ymnis, handler, serializer, viewModel);

            return viewModel;
        }

        private static void AppendAnnotation(ItemText annotation, IRuleHandler handler, ElementViewModel viewModel)
        {
            if (annotation?.IsEmpty == false)
            {
                viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Text, new List<string> { annotation[handler.Settings.Language] }));
            }
        }

        private static void AppendIhos(int ihos, Prosomoion prosomoion, IRuleHandler handler, 
            IRuleSerializerRoot serializer, ElementViewModel viewModel)
        {
            TypiconEntity typ = handler.Settings.Rule.Owner;
            //текст "Глас"
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = serializer };
            req.Key = CommonRuleConstants.IhosText;

            string ihosString = $"{typ.GetCommonRuleTextValue(req, handler.Settings.Language)} {ihos}. ";

            //самоподобен?
            if (prosomoion?.Self == true)
            {
                req.Key = CommonRuleConstants.SelfText;
                ihosString += typ.GetCommonRuleTextValue(req, handler.Settings.Language);
            }
            //если подобен
            else if (prosomoion?.IsEmpty == false)
            {
                req.Key = CommonRuleConstants.ProsomoionText;
                string p = typ.GetCommonRuleTextValue(req, handler.Settings.Language);

                ihosString += $"{p}: \"{ prosomoion[handler.Settings.Language] }\"";
            }

            viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Text, new List<string> { ihosString }));
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
                        new List<string> { stihos[handler.Settings.Language] }));
                }

                viewModel.Add(ViewModelItemFactory.Create(TextHolderKind.Choir, text.ChoirText,
                    new List<string> { ymnos.Text[handler.Settings.Language] } ));
            }
        }

        private static (string StihosText, string ChoirText) GetStringValues(IRuleHandler handler, IRuleSerializerRoot serializer)
        {
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = serializer };

            //находим Стих и Хор для дальнешей вставки
            req.Key = CommonRuleConstants.StihosRule;
            string stihos = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);

            req.Key = CommonRuleConstants.ChoirRule;
            string choir = handler.Settings.Rule.Owner.GetCommonRuleTextValue(req, handler.Settings.Language);

            return (stihos, choir);
        }
    }
}
