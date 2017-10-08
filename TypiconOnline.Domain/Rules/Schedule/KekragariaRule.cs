using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    public class KekragariaRule : YmnosStructureRule
    {
        private ItemBoolean _showPsalm;

        public KekragariaRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.ShowPsalmAttribute];

            _showPsalm = new ItemBoolean((attr != null) ? attr.Value : "false");
        }

        #region Properties

        /// <summary>
        /// Признак, показывать ли 140 псалом
        /// </summary>
        public ItemBoolean ShowPsalm
        {
            get
            {
                return _showPsalm;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KekragariaRule>())
            {
                base.InnerInterpret(date, handler);


            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (!_showPsalm.IsValid)
            {
                AppendAllBrokenConstraints(_showPsalm);
            }
        }
    }
}
