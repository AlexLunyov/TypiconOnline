using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ModifyReplacedDay : ModifyDay
    {
        public ModifyReplacedDay(string name) : base(name) { }

        public ModifyReplacedDay(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.KindAttrName];

            if (Enum.TryParse(attr.Value, true, out KindOfReplacedDay value))
            {
                Kind = value;
            }
        }

        #region Properties

        public KindOfReplacedDay Kind { get; set; }

        /// <summary>
        /// Дата, по которой будет совершаться поиск правила для модификации
        /// </summary>
        public DateTime DateToReplaceCalculated
        {
            get; private set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            InterpretChildDateExp(date, handler);

            DateToReplaceCalculated = date;

            handler.Execute(this);

            ModifyReplacedDay?.Interpret(MoveDateCalculated, handler);
        }

    }
}
