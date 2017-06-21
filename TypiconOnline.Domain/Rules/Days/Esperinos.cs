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
    /// Вечерня
    /// </summary>
    public class Esperinos : MikrosEsperinos
    {
        public Esperinos(XmlNode node) : base(node)
        {
            //Prokeimenon
            XmlNode elementNode = node.SelectSingleNode(RuleConstants.ProkeimenonNode);
            if (elementNode != null)
            {
                Prokeimenon = new Prokeimenon(elementNode);
            }

            //Liti
            elementNode = node.SelectSingleNode(RuleConstants.LitiNode);
            if (elementNode != null)
            {
                Liti = new Stichera(elementNode);
            }
        }

        #region Properties

        
        /// <summary>
        /// Прокимен на вечерне
        /// </summary>
        public Prokeimenon Prokeimenon { get; set; }
        /// <summary>
        /// Стихиры на литии
        /// </summary>
        public Stichera Liti { get; set; }
        

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            base.Validate();

            if (Prokeimenon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prokeimenon, ElementName + "." + RuleConstants.ProkeimenonNode);
            }

            if (Liti?.IsValid == false)
            {
                AppendAllBrokenConstraints(Liti, ElementName + "." + RuleConstants.LitiNode);
            }
        }
    }
}
