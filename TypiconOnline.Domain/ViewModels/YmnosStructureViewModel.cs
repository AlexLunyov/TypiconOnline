using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.ViewModels
{
    public abstract class YmnosStructureViewModel : ContainerViewModel
    {
        protected YmnosStructureRule _rule;

        public YmnosStructureKind Kind { get; set; }

        public int Ihos { get; set; }
        /// <summary>
        /// "Глас" текст
        /// </summary>
        public string IhosText { get; set; }

        public YmnosStructureViewModel(YmnosStructureRule rule, IRuleHandler handler) 
        {
            if (rule == null || rule.CalculatedYmnosStructure == null) throw new ArgumentNullException("YmnosStructureRule");
            if (handler == null) throw new ArgumentNullException("handler");

            _rule = rule;
            _handler = handler;

            Kind = _rule.Kind;
        }

        protected override void FillChildElements()
        {
            //здесь вставляется индивидуальная обработка наследников
            ConstructForm(_handler);

            //а теперь добавляем стихиры, общие для всех наследников данного класса
            YmnosStructure ymnosStructure = _rule.CalculatedYmnosStructure;

            //Groups
            for (int i = 0; i < ymnosStructure.Groups.Count; i++)
            {
                YmnosGroup group = ymnosStructure.Groups[i];

                YmnosGroupViewModel item = new YmnosGroupViewModel(group, _handler);

                if (i == 0)
                {
                    Ihos = group.Ihos;
                    IhosText = item.IhosText;
                }

                _childElements.AddRange(item.ChildElements);
            }

            SetStringCommonRules(ymnosStructure, _handler);

            //Doxastichon
            if (ymnosStructure.Doxastichon != null)
            {
                _childElements.AddRange(new YmnosGroupViewModel(ymnosStructure.Doxastichon, _handler).ChildElements);
            }
            //Theotokion
            if (ymnosStructure.Theotokion?.Count > 0)
            {
                _childElements.AddRange(new YmnosGroupViewModel(ymnosStructure.Theotokion[0], _handler).ChildElements);
            }
        }

        private void SetStringCommonRules(YmnosStructure ymnosStructure, IRuleHandler handler)
        {
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { Handler = handler };

            //добавляем стихи к славнику и богородичну
            if (ymnosStructure.Doxastichon != null)
            {
                //слава
                req.Key = CommonRuleConstants.SlavaText;
                ymnosStructure.Doxastichon.Ymnis[0].Stihoi.Add(CommonRuleService.Instance.GetItemTextValue(req));
                //и ныне
                if (ymnosStructure.Theotokion?.Count > 0)
                {
                    req.Key = CommonRuleConstants.InyneText;
                    ymnosStructure.Theotokion[0].Ymnis[0].Stihoi.Add(CommonRuleService.Instance.GetItemTextValue(req));
                }
            }
            else
            {
                //слава и ныне
                if (ymnosStructure.Theotokion?.Count > 0)
                {
                    req.Key = CommonRuleConstants.SlavaInyneText;
                    ymnosStructure.Theotokion[0].Ymnis[0].Stihoi.Add(CommonRuleService.Instance.GetItemTextValue(req));
                }
            }
        }

        /// <summary>
        /// Метод определяется в наследниках. Конструирует уникальную выходную форму для кажждого класса в отдельности
        /// </summary>
        protected abstract void ConstructForm(IRuleHandler handler);
    }
}
