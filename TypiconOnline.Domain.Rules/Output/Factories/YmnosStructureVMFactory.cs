using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Output.Factories
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

            OutputSectionModelCollection viewModel = new OutputSectionModelCollection();

            //здесь вставляется индивидуальная обработка наследников
            AppendCustomForm(req/*, viewModel*/);

            //а теперь добавляем стихиры, общие для всех наследников данного класса
            YmnosStructure ymnosStructure = req.Element.Structure;

            //Groups
            foreach (var group in ymnosStructure.Groups)
            {
                viewModel.AddRange(group.GetViewModel(req.Handler, Serializer));
            }

            SetStringCommonRules(ymnosStructure, req.Handler.Settings.TypiconVersionId);

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

        private void SetStringCommonRules(YmnosStructure ymnosStructure, int typiconId)
        {
            //добавляем стихи к славнику и богородичну
            if (ymnosStructure.Doxastichon != null)
            {
                //слава
                AddPredefinedStihos(ymnosStructure.Doxastichon.Ymnis[0].Stihoi, CommonRuleConstants.SlavaText);

                //и ныне
                if (ymnosStructure.Theotokion?.Count > 0)
                {
                    AddPredefinedStihos(ymnosStructure.Theotokion[0].Ymnis[0].Stihoi, CommonRuleConstants.InyneText);
                }
            }
            //слава и ныне
            else if (ymnosStructure.Theotokion?.Count > 0)
            {
                AddPredefinedStihos(ymnosStructure.Theotokion[0].Ymnis[0].Stihoi, CommonRuleConstants.SlavaInyneText);
            }

            void AddPredefinedStihos(ICollection<ItemText> stihoi, string key)
            {
                var itemText = Serializer.GetCommonRuleItemTextValue(typiconId, key);

                stihoi.Add(itemText);
            }
        }

        /// <summary>
        /// Метод определяется в наследниках. Конструирует уникальную выходную форму для кажждого класса в отдельности
        /// </summary>
        protected abstract void AppendCustomForm(CreateViewModelRequest<YmnosStructureRule> req/*, ElementViewModel viewModel*/);
    }
}
