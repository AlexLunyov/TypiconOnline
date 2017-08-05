using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Описание стихир Господи воззвах в последовательности богослужений
    /// </summary>
    public class KekragariaRule : ExecContainer, ICustomInterpreted
    {
        private Stichera _stichera;
        private ItemBoolean _showPsalm;

        public KekragariaRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.ShowPsalmAttribute];

            _showPsalm = new ItemBoolean((attr != null) ? attr.Value : "false");
        }

        #region Properties

        /// <summary>
        /// Вычисленная последовательность стихир на Господи воззвах
        /// </summary>
        public Stichera CalculatedStichera
        {
            get
            {
                return _stichera;
            }
        }

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
            if (IsValid && handler.IsAuthorized<KekragariaRule>())
            {
                    
            }
        }

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }
    }
}
