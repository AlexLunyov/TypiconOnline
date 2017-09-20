using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    /// <summary>
    /// Вечерня
    /// </summary>
    [Serializable]
    public class Esperinos : MikrosEsperinos
    {
        public Esperinos() { }

        public Esperinos(XmlNode node) : base(node)
        {
            //Prokeimeni
            XmlNodeList prokiemenonList = node.SelectNodes(RuleConstants.ProkeimenonNode);
            if (prokiemenonList != null)
            {
                Prokeimeni = new List<Prokeimenon>();

                foreach (XmlNode prokiemenonItemNode in prokiemenonList)
                {
                    Prokeimeni.Add(new Prokeimenon(prokiemenonItemNode));
                }
            }

            //Liti
            XmlNode elementNode = node.SelectSingleNode(RuleConstants.LitiNode);
            if (elementNode != null)
            {
                Liti = new YmnosStructure(elementNode);
            }
        }

        #region Properties

        /// <summary>
        /// Прокимны на вечерне.
        /// Два прокимна бывает Великим постом
        /// </summary>
        [XmlElement(RuleConstants.ProkeimenonNode)]
        public List<Prokeimenon> Prokeimeni { get; set; }
        /// <summary>
        /// Стихиры на литии
        /// </summary>
        [XmlElement(RuleConstants.LitiNode)]
        public YmnosStructure Liti { get; set; }
        

        #endregion

        protected override void Validate()
        {
            base.Validate();

            if (Prokeimeni != null)
            {
                Prokeimeni.ForEach(c =>
                {
                    if (!c.IsValid)
                    {
                        AppendAllBrokenConstraints(c, /*ElementName + "." + */RuleConstants.ProkeimenonNode);
                    }
                });
            }

            if (Liti?.IsValid == false)
            {
                AppendAllBrokenConstraints(Liti, /*ElementName + "." + */RuleConstants.LitiNode);
            }
        }
    }
}
