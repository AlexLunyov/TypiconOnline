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

            //группы стихир
            XmlNodeList groupList = node.SelectNodes(RuleConstants.SticheraGroupNode);
            if (groupList != null)
            {
                foreach (XmlNode groupItemNode in groupList)
                {
                    Groups.Add(new YmnosGroup(groupItemNode));
                }
            }

            //славник
            XmlNode doxastichonNode = node.SelectSingleNode(RuleConstants.SticheraDoxastichonNode);
            if (doxastichonNode != null)
            {
                Doxastichon = new YmnosGroup(doxastichonNode);
            }

            //богородичен
            XmlNode theotokionNode = node.SelectSingleNode(RuleConstants.SticheraTheotokionNode);
            if (theotokionNode != null)
            {
                Theotokion = new YmnosGroup(theotokionNode);
            }
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
            foreach (YmnosGroup group in Groups)
            {
                if (!group.IsValid)
                {
                    AppendAllBrokenConstraints(group, ElementName + "." + RuleConstants.SticheraGroupNode);
                }
            }

            if (Doxastichon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Doxastichon, ElementName + "." + RuleConstants.SticheraDoxastichonNode);
            }

            if (Theotokion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Theotokion, ElementName + "." + RuleConstants.SticheraTheotokionNode);
            }
        }
    }
}
