using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для стихир на Хвалитех
    /// </summary>
    public class AinoiRule : KekragariaRule
    {
        public AinoiRule(XmlNode node) : base(node) { }
    }
}
