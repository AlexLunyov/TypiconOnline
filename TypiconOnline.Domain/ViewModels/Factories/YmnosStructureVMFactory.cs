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
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.ViewModels.Factories
{
    public abstract class YmnosStructureVMFactory : ViewModelFactoryBase<YmnosStructureRule>
    {
        public YmnosStructureVMFactory(IRuleSerializerRoot serializer) : base(serializer) { }


        public override void Create(CreateViewModelRequest<YmnosStructureRule> req)
        {
            if (req.Element == null
                || req.Element.Structure == null)
            {
                //TODO: просто ничего не делаем, хотя надо бы это обрабатывать
                return;
            }

            ElementViewModel viewModel = new ElementViewModel();

            //здесь вставляется индивидуальная обработка наследников
            AppendCustomForm(req/*, viewModel*/);

            //а теперь добавляем стихиры, общие для всех наследников данного класса
            YmnosStructure ymnosStructure = req.Element.Structure;

            //Groups
            foreach (var group in ymnosStructure.Groups)
            {
                viewModel.AddRange(group.GetViewModel(req.Handler, Serializer));
            }

            SetStringCommonRules(ymnosStructure, req.Handler.Settings.Rule.Owner);

            //Doxastichon
            if (ymnosStructure.Doxastichon != null)
            {
                viewModel.AddRange(ymnosStructure.Doxastichon.GetViewModel(req.Handler, Serializer));
            }
            //Theotokion
            if (ymnosStructure.Theotokion?.Count > 0)
            {
                viewModel.AddRange(ymnosStructure.Theotokion[0].GetViewModel(req.Handler, Serializer));
            }

            req.AppendModelAction(viewModel);
        }

        private void SetStringCommonRules(YmnosStructure ymnosStructure, TypiconEntity typicon)
        {
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
        protected abstract void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req/*, ElementViewModel viewModel*/);
    }
}
