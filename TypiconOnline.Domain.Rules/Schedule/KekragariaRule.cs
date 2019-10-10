using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для стихир на Господи воззвах
    /// </summary>
    public class KekragariaRule : YmnosStructureRule
    {
        public KekragariaRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, string name) 
            : base(viewModelFactory, name) { }

        #region Properties

        /// <summary>
        /// Признак, показывать ли 140 псалом
        /// </summary>
        public bool ShowPsalm { get; set; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsTypeAuthorized(this))
            {
                base.InnerInterpret(handler);
            }
        }

        //protected override void Validate()
        //{
        //    base.Validate();

        //    if (!_showPsalm.IsValid)
        //    {
        //        AppendAllBrokenConstraints(_showPsalm);
        //    }
        //}

        
    }
}
