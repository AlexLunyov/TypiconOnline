using System;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание песни из блаженн
    /// </summary>
    public class MakariosOdi : RuleElement
    {
        public MakariosOdi(XmlNode node) : base(node)
        {

        }

        #region Properties
        /// <summary>
        /// Номер песни
        /// </summary>
        public ItemInt Number { get; set; }
        /// <summary>
        /// Количество стихов
        /// </summary>
        public ItemInt Count { get; set; }

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