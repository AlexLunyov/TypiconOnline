using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

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
            if (handler.IsAuthorized<KekragariaRule>())
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
