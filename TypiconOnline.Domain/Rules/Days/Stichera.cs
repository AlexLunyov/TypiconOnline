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
    /// Класс описывает коолекцию песнопений.
    /// Таквыми являются Стихиры на Господи воззвах, на Хвалитех, и т.д.
    /// </summary>
    public class Stichera : RuleElement
    {
        public Stichera(XmlNode node) : base(node)
        {
            Groups = new List<YmnosGroup>();
            //Doxastichon = new YmnosGroup();
            //Theotokion = new YmnosGroup();
        }

        #region Properties
        public List<YmnosGroup> Groups { get; set; }
        /// <summary>
        /// Славник
        /// </summary>
        public YmnosGroup Doxastichon { get; set; }
        /// <summary>
        /// Богородичен
        /// </summary>
        public YmnosGroup Theotokion { get; set; }
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
