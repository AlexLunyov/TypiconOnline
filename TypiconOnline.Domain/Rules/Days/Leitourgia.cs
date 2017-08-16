using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание Литургийных чтений
    /// </summary>
    public class Leitourgia : RuleElement
    {
        public Leitourgia(XmlNode node) : base(node)
        {

        }

        #region Properties


        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            //TODO: не реализовано
            //throw new NotImplementedException();
        }
    }
}
