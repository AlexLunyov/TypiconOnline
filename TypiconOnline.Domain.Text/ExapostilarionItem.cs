using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Элемент структуры эксапостилария
    /// </summary>
    [Serializable]
    public class ExapostilarionItem : DayElementBase
    {
        #region Properties
        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        [XmlElement(XmlConstants.ProsomoionNode)]
        public Prosomoion Prosomoion { get; set; }

        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(XmlConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }

        /// <summary>
        /// Текст песнопения
        /// </summary>
        [XmlElement(XmlConstants.YmnosTextNode)]
        public ItemText Text { get; set; }
        #endregion

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
