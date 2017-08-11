using System;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Days
{
    public class Orthros : RuleElement
    {
        public Orthros(XmlNode node) : base(node)
        {
            //SedalenKathizma1
            XmlNode elementNode = node.SelectSingleNode(RuleConstants.SedalenKathizma1Node);
            if (elementNode != null)
            {
                SedalenKathizma1 = new YmnosStructure(elementNode);
            }

            //SedalenKathizma2
            elementNode = node.SelectSingleNode(RuleConstants.SedalenKathizma2Node);
            if (elementNode != null)
            {
                SedalenKathizma2 = new YmnosStructure(elementNode);
            }

            //SedalenKathizma3
            elementNode = node.SelectSingleNode(RuleConstants.SedalenKathizma3Node);
            if (elementNode != null)
            {
                SedalenKathizma3 = new YmnosStructure(elementNode);
            }

            //SedalenPolyeleos
            elementNode = node.SelectSingleNode(RuleConstants.SedalenPolyeleosNode);
            if (elementNode != null)
            {
                SedalenPolyeleos = new YmnosStructure(elementNode);
            }

            //Megalynarion
            elementNode = node.SelectSingleNode(RuleConstants.MegalynarionNode);
            if (elementNode != null)
            {
                Megalynarion = new ItemTextCollection(elementNode.OuterXml);
            }

            //Eclogarion
            elementNode = node.SelectSingleNode(RuleConstants.EclogarionNode);
            if (elementNode != null)
            {
                Eclogarion = new ItemTextCollection(elementNode.OuterXml);
            }

            //Prokeimenon
            elementNode = node.SelectSingleNode(RuleConstants.ProkeimenonNode);
            if (elementNode != null)
            {
                Prokeimenon = new Prokeimenon(elementNode);
            }

            //Evangelion
            elementNode = node.SelectSingleNode(RuleConstants.EvangelionNode);
            if (elementNode != null)
            {
                Evangelion = new EvangelionReading(elementNode);
            }

            //Sticheron50
            elementNode = node.SelectSingleNode(RuleConstants.Sticheron50Node);
            if (elementNode != null)
            {
                Sticheron50 = new YmnosStructure(elementNode);
            }

            //Kanonas
            elementNode = node.SelectSingleNode(RuleConstants.KanonasNode);
            if (elementNode != null)
            {
                Kanonas = new Kanonas(elementNode);
            }

            //Ainoi
            elementNode = node.SelectSingleNode(RuleConstants.AinoiNode);
            if (elementNode != null)
            {
                Ainoi = new YmnosStructure(elementNode);
            }

            //Aposticha
            elementNode = node.SelectSingleNode(RuleConstants.ApostichaNode);
            if (elementNode != null)
            {
                Aposticha = new YmnosStructure(elementNode);
            }
        }

        #region Properties

        /// <summary>
        /// седален по 1-ой кафизме
        /// </summary>
        public YmnosStructure SedalenKathizma1 { get; set; }
        /// <summary>
        /// седален по 2-ой кафизме
        /// </summary>
        public YmnosStructure SedalenKathizma2 { get; set; }
        /// <summary>
        /// седален по 3-ой кафизме
        /// </summary>
        public YmnosStructure SedalenKathizma3 { get; set; }
        /// <summary>
        /// седален по полиелее
        /// </summary>
        public YmnosStructure SedalenPolyeleos { get; set; }
        /// <summary>
        /// Величания
        /// </summary>
        public ItemTextCollection Megalynarion { get; set; }
        /// <summary>
        /// Псалом избранный
        /// </summary>
        public ItemTextCollection Eclogarion { get; set; }
        /// <summary>
        /// Прокимен на полиелее
        /// </summary>
        public Prokeimenon Prokeimenon { get; set; }
        /// <summary>
        /// Евангельское чтение
        /// </summary>
        public EvangelionReading Evangelion { get; set; }
        /// <summary>
        /// Стихира по 50-м псалме
        /// </summary>
        public YmnosStructure Sticheron50 { get; set; }
        /// <summary>
        /// Канон
        /// </summary>
        public Kanonas Kanonas { get; set; }
        /// <summary>
        /// Стихиры на Хвалитех
        /// </summary>
        public YmnosStructure Ainoi { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        public YmnosStructure Aposticha { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            if (SedalenKathizma1?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathizma1, ElementName + "." + RuleConstants.SedalenKathizma1Node);
            }

            if (SedalenKathizma2?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathizma2, ElementName + "." + RuleConstants.SedalenKathizma2Node);
            }

            if (SedalenKathizma3?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathizma3, ElementName + "." + RuleConstants.SedalenKathizma3Node);
            }

            if (SedalenPolyeleos?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenPolyeleos, ElementName + "." + RuleConstants.SedalenPolyeleosNode);
            }

            if (Megalynarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Megalynarion, ElementName + "." + RuleConstants.MegalynarionNode);
            }

            if (Eclogarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Eclogarion, ElementName + "." + RuleConstants.EclogarionNode);
            }

            if (Prokeimenon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prokeimenon, ElementName + "." + RuleConstants.ProkeimenonNode);
            }

            if (Evangelion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Evangelion, ElementName + "." + RuleConstants.EvangelionNode);
            }

            if (Sticheron50?.IsValid == false)
            {
                AppendAllBrokenConstraints(Sticheron50, ElementName + "." + RuleConstants.Sticheron50Node);
            }

            if (Kanonas?.IsValid == false)
            {
                AppendAllBrokenConstraints(Kanonas, ElementName + "." + RuleConstants.KanonasNode);
            }

            if (Ainoi?.IsValid == false)
            {
                AppendAllBrokenConstraints(Ainoi, ElementName + "." + RuleConstants.AinoiNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, ElementName + "." + RuleConstants.ApostichaNode);
            }
        }
    }
}