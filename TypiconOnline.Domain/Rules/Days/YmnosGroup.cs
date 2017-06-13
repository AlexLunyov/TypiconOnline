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
    /// <summary>
    /// Песнопения, сгруппированные по гласу и подобнам
    /// </summary>
    public class YmnosGroup : RuleElement
    {
        public YmnosGroup(XmlNode node) : base(node)
        {
            Prosomoion = new Prosomoion();
            Annotation = new ItemText();
            Ymnis = new List<Ymnos>();
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        public int Ihos { get; set; }

        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        public Prosomoion Prosomoion { get; set; }

        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        public ItemText Annotation { get; set; }

        public List<Ymnos> Ymnis { get; set; }

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
