using System;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Базовый класс для описания правил стихир в последовательности богослужений
    /// </summary>
    public abstract class YmnosStructureRule : StructureRuleBase<YmnosStructure, IYmnosStructureRuleElement>, IViewModelElement
    {
        public YmnosStructureRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, string name) : base(name)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in YmnosStructureRule");
        }

        #region Properties

        /// <summary>
        /// Тип структуры (Господи воззвах, стихиры на стиховне и т.д.)
        /// </summary>
        //public YmnosStructureKind Kind { get; set; }

        /// <summary>
        /// Общее количество песнопений (ограничение)
        /// </summary>
        public int TotalYmnosCount { get; set; }

        protected IElementViewModelFactory<YmnosStructureRule> ViewModelFactory { get; }

        #endregion

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }

        protected override bool IsAuthorized(IRuleHandler handler) => handler.IsAuthorized<YmnosStructureRule>();

        public virtual void CreateViewModel(IRuleHandler handler, Action<OutputElementCollection> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<YmnosStructureRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }
    }
}
