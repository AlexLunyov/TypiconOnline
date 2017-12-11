using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public class KekragariaRuleVMFactory : YmnosStructureVMFactory
    {
        public KekragariaRuleVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }

        protected override void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req/*, ElementViewModel viewModel*/)
        {
            ConstructWithCommonRule(req, CommonRuleConstants.KekragariaRule);
        }

        protected virtual void ConstructWithCommonRule(CreateViewModelRequest<YmnosStructureRule> req, string key)
        {
            List<RuleElement> children = req.Handler.Settings.Rule.Owner.GetCommonRuleChildren(
                new CommonRuleServiceRequest() { Key = key, RuleSerializer = Serializer }).ToList();

            if (req.Element.Structure.Groups.Count > 0)
            {
                //заполняем header - вставляем номер гласа
                ItemText header = (children[0] as TextHolder).Paragraphs[0];
                string headerText = header.StringExpression;
                header.StringExpression = headerText.Replace("[ihos]", 
                    req.Handler.Settings.Language.IntConverter.ToString(req.Element.Structure.Groups[0].Ihos));

                //а теперь отсчитываем от последней стихиры и добавляем к ней стих из псалма
                //сам стих удаляем из псалма

                TextHolder psalm = new TextHolder(children[2] as TextHolder);

                for (int i = req.Element.Structure.Groups.Count - 1; i >= 0; i--)
                {
                    YmnosGroup group = req.Element.Structure.Groups[i];

                    for (int n = group.Ymnis.Count - 1; n >= 0; n--)
                    {
                        Ymnos ymnos = group.Ymnis[n];

                        ItemTextNoted stihos = psalm.Paragraphs.Last();

                        ymnos.Stihoi.Add(stihos);

                        psalm.Paragraphs.Remove(stihos);
                    }
                }
            }

            //теперь вставляем шапку
            AppendItem(children[0] as TextHolder);
            AppendItem(children[1] as TextHolder);

            //вставляем псалмы
            if ((req.Element as KekragariaRule).ShowPsalm)
            {
                AppendItem(children[2] as TextHolder);
            }

            void AppendItem(TextHolder textHolder)
            {
                req.AppendModelAction(new ElementViewModel()
                    { ViewModelItemFactory.Create(textHolder, req.Handler, Serializer) });
            }
        }
    }
}
