using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Описание эксапостилария, находящегося после 9-й песни канона утрени
    /// </summary>
    [Serializable]
    public class Exapostilarion : ValueObjectBase
    {
        #region Properties

        /// <summary>
        /// Коллекция песнопений
        /// </summary>
        [XmlElement(RuleConstants.KanonasExapostilarionYmnosNode)]
        public List<ExapostilarionItem> Ymnis { get; set; }

        /// <summary>
        /// Славник
        /// </summary>
        [XmlElement(RuleConstants.YmnosStructureDoxastichonNode)]
        public ExapostilarionItem Doxastichon { get; set; }
        /// <summary>
        /// Богородичен
        /// </summary>
        [XmlElement(RuleConstants.YmnosStructureTheotokionNode)]
        public List<ExapostilarionItem> Theotokion { get; set; }

        #endregion

        protected override void Validate()
        {
            //nothing yet
        }
    }
}
