using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyReplacedDay : ModifyDay
    {
        private RuleConstants.KindOfReplacedDay _kind = RuleConstants.KindOfReplacedDay.undefined;
        private DateTime _dateToReplaceCalculated;

        public ModifyReplacedDay(XmlNode node) : base(node)
        {
            if (node.Attributes != null)
            {
                XmlAttribute attr = node.Attributes[RuleConstants.KindAttrName];

                if (attr != null)
                {
                    Enum.TryParse(attr.Value, out _kind);
                }
            }
        }

        #region Properties

        public RuleConstants.KindOfReplacedDay Kind
        {
            get
            {
                return _kind;
            }
        }

        /// <summary>
        /// Дата, по которой будет совершаться поиск правила для модификации
        /// </summary>
        public DateTime DateToReplaceCalculated
        {
            get
            {
                return _dateToReplaceCalculated;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (IsValid)
            {
                InterpretChildDateExp(date, handler);

                _dateToReplaceCalculated = date;

                handler.Execute(this);

                if (_modifyReplacedDay != null)
                {
                    _modifyReplacedDay.Interpret(MoveDateCalculated, handler);
                }
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (_kind == RuleConstants.KindOfReplacedDay.undefined)
            {
                AddBrokenConstraint(ModifyReplacedDayBusinessConstraint.KindUndefined, ElementName);
            }
        }
    }
}
