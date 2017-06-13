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
    public class Prokeimenon : RuleElement
    {
        public Prokeimenon(XmlNode node) : base(node)
        {
            StihosCollection = new List<ItemText>();
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        public int Ihos { get; set; }
        /// <summary>
        /// Стихи
        /// </summary>
        public List<ItemText> StihosCollection { get; set; }

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
