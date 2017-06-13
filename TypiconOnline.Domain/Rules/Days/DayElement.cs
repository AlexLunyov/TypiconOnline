using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    public class DayElement : RuleElement
    {
        public DayElement(XmlNode node) : base(node)
        {
            Name = new ItemText();
        }

        #region Properties
        /// <summary>
        /// Наименование праздника.
        /// Например, "Великомученика Никиты."
        /// </summary>
        public ItemText Name { get; set; }
        /// <summary>
        /// Описание службы вечерни
        /// </summary>
        public Esperinos Esperinos { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
