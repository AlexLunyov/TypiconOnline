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
    /// Вечерня
    /// </summary>
    public class Esperinos : RuleElement
    {
        public Esperinos(XmlNode node) : base(node)
        {
            //Kekragaria = new Stichera();
        }

        #region Properties

        /// <summary>
        /// Господи воззвах
        /// </summary>
        public Stichera Kekragaria { get; set; }
        /// <summary>
        /// Прокимен на вечерне
        /// </summary>
        public Prokeimenon Prokeimenon { get; set; }
        /// <summary>
        /// Стихиры на литии
        /// </summary>
        public Stichera Liti { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        public Stichera Aposticha { get; set; }

        /// <summary>
        /// Отпустительный тропарь
        /// </summary>
        public YmnosGroup Troparion { get; set; }

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
