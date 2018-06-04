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

namespace TypiconOnline.Domain.Rules.ViewModels
{
    public abstract class YmnosStructureViewModel : ContainerViewModel
    {
        protected YmnosStructureRule _rule;

        public YmnosStructureViewModel(YmnosStructureRule rule, IRuleHandler handler) 
        {
            if (rule == null || rule.Structure == null) throw new ArgumentNullException("YmnosStructureRule");
            _rule = rule;

            Serializer = rule.Serializer;

            _handler = handler ?? throw new ArgumentNullException("handler");
        }
        
        /// <summary>
        /// Номер гласа структуры песнопений
        /// </summary>
        public int Ihos { get; private set; }
        /// <summary>
        /// "Глас" текст
        /// </summary>
        public string IhosText { get; private set; }

        public YmnosStructureKind Kind
        {
            get
            {
                return (_rule != null) ? _rule.Kind : default(YmnosStructureKind);
            }
        }

        public IRuleSerializerRoot Serializer { get; }

        protected override void FillChildElements()
        {
            //здесь вставляется индивидуальная обработка наследников
            ConstructForm(_handler);

            //а теперь добавляем стихиры, общие для всех наследников данного класса
            YmnosStructure ymnosStructure = _rule.Structure;

            //Groups
            for (int i = 0; i < ymnosStructure.Groups.Count; i++)
            {
                YmnosGroup group = ymnosStructure.Groups[i];

                YmnosGroupViewModel item = new YmnosGroupViewModel(group, _handler, Serializer);

                if (i == 0)
                {
                    Ihos = group.Ihos;
                    IhosText = item.IhosText;
                }

                _childElements.AddRange(item.ChildElements);
            }

            SetStringCommonRules(ymnosStructure);

            //Doxastichon
            if (ymnosStructure.Doxastichon != null)
            {
                _childElements.AddRange(new YmnosGroupViewModel(ymnosStructure.Doxastichon, _handler, Serializer).ChildElements);
            }
            //Theotokion
            if (ymnosStructure.Theotokion?.Count > 0)
            {
                _childElements.AddRange(new YmnosGroupViewModel(ymnosStructure.Theotokion[0], _handler, Serializer).ChildElements);
            }
        }

        private void SetStringCommonRules(YmnosStructure ymnosStructure)
        {
            TypiconEntity typicon =  _handler.Settings.Rule.Owner;
            CommonRuleServiceRequest req = new CommonRuleServiceRequest() { RuleSerializer = Serializer };

            //добавляем стихи к славнику и богородичну
            if (ymnosStructure.Doxastichon != null)
            {
                //слава
                req.Key = CommonRuleConstants.SlavaText;
                ymnosStructure.Doxastichon.Ymnis[0].Stihoi.Add(typicon.GetCommonRuleItemTextValue(req));
                //и ныне
                if (ymnosStructure.Theotokion?.Count > 0)
                {
                    req.Key = CommonRuleConstants.InyneText;
                    ymnosStructure.Theotokion[0].Ymnis[0].Stihoi.Add(typicon.GetCommonRuleItemTextValue(req));
                }
            }
            else
            {
                //слава и ныне
                if (ymnosStructure.Theotokion?.Count > 0)
                {
                    req.Key = CommonRuleConstants.SlavaInyneText;
                    ymnosStructure.Theotokion[0].Ymnis[0].Stihoi.Add(typicon.GetCommonRuleItemTextValue(req));
                }
            }
        }

        /// <summary>
        /// Метод определяется в наследниках. Конструирует уникальную выходную форму для кажждого класса в отдельности
        /// </summary>
        protected abstract void ConstructForm(IRuleHandler handler);
    }
}
