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

namespace TypiconOnline.Domain.ViewModels
{
    public class KekragariaRuleViewModel : YmnosStructureViewModel
    {
        //private KekragariaRule _rule;

        public KekragariaRuleViewModel(YmnosStructureRule rule, IRuleHandler handler) : base(rule, handler)
        {
            if (!(rule is KekragariaRule)) throw new InvalidCastException("KekragariaRule");

            //_rule = rule as KekragariaRule;
        }

        protected override void ConstructForm(IRuleHandler handler)
        {
            ConstructWithCommonRule(handler, CommonRuleConstants.KekragariaRule);
        }

        protected virtual void ConstructWithCommonRule(IRuleHandler handler, string key)
        {
            List<RuleElement> children = CommonRuleService.Instance.GetCommonRuleChildren(
                new CommonRuleServiceRequest() { Handler = handler, Key = key });

            if (_rule.CalculatedYmnosStructure.Groups.Count > 0)
            {
                //заполняем header - вставляем номер гласа
                ItemText header = (children[0] as TextHolder).Paragraphs[0];
                string headerText = header.StringExpression;
                header.StringExpression = headerText.Replace("[ihos]", _rule.CalculatedYmnosStructure.Groups[0].Ihos.ToString());

                //а теперь отсчитываем от последней стихиры и добавляем к ней стих из псалма
                //сам стих удаляем из псалма

                TextHolder psalm = new TextHolder(children[2] as TextHolder);

                for (int i = _rule.CalculatedYmnosStructure.Groups.Count - 1; i >= 0; i--)
                {
                    YmnosGroup group = _rule.CalculatedYmnosStructure.Groups[i];

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
            _childElements.Add( new TextHolderViewModel(children[0] as TextHolder, handler) );
            _childElements.Add( new TextHolderViewModel(children[1] as TextHolder, handler) );


            //вставляем псалмы
            if ((_rule as KekragariaRule).ShowPsalm.Value)
            {
                _childElements.Add(new TextHolderViewModel(children[2] as TextHolder, handler));
            }
        }
    }
}
