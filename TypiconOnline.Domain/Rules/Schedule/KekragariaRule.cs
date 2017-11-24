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

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для стихир на Господи воззвах
    /// </summary>
    public class KekragariaRule : YmnosStructureRule
    {
        private ItemBoolean _showPsalm;

        public KekragariaRule(string name) : base(name) { }

        public KekragariaRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.ShowPsalmAttribute];

            _showPsalm = new ItemBoolean((attr != null) ? attr.Value : "false");
        }

        #region Properties

        /// <summary>
        /// Признак, показывать ли 140 псалом
        /// </summary>
        public bool ShowPsalm { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KekragariaRule>())
            {
                base.InnerInterpret(date, handler);
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

        public override ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new KekragariaRuleViewModel(this, handler);
        }
    }
}
