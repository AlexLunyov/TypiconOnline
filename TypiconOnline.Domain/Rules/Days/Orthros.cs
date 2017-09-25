using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
{
    [Serializable]
    public class Orthros : ValueObjectBase
    {
        public Orthros() { }

        public Orthros(XmlNode node) 
        {
            //SedalenKathisma1
            XmlNode elementNode = node.SelectSingleNode(RuleConstants.SedalenKathisma1Node);
            if (elementNode != null)
            {
                SedalenKathisma1 = new YmnosStructure(elementNode);
            }

            //SedalenKathisma2
            elementNode = node.SelectSingleNode(RuleConstants.SedalenKathisma2Node);
            if (elementNode != null)
            {
                SedalenKathisma2 = new YmnosStructure(elementNode);
            }

            //SedalenKathisma3
            elementNode = node.SelectSingleNode(RuleConstants.SedalenKathisma3Node);
            if (elementNode != null)
            {
                SedalenKathisma3 = new YmnosStructure(elementNode);
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
            string xPath = string.Format("{0}/{1}", RuleConstants.EvangelionNode, RuleConstants.EvangelionPartNode);
            XmlNodeList evangelionList = node.SelectNodes(xPath);
            if (evangelionList != null)
            {
                Evangelion = new List<EvangelionPart>();

                foreach (XmlNode ymnosItemNode in evangelionList)
                {
                    Evangelion.Add(new EvangelionPart(ymnosItemNode));
                }
            }

            //Sticheron50
            elementNode = node.SelectSingleNode(RuleConstants.Sticheron50Node);
            if (elementNode != null)
            {
                Sticheron50 = new YmnosGroup(elementNode);
            }

            //Kanonas
            xPath = string.Format("{0}/{1}", RuleConstants.KanonesNode, RuleConstants.KanonasNode);
            XmlNodeList kanonesList = node.SelectNodes(xPath);
            if (kanonesList != null)
            {
                Kanones = new List<Days.Kanonas>();

                foreach (XmlNode kanonasNode in kanonesList)
                {
                    Kanones.Add(new Kanonas(kanonasNode));
                }
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
        [XmlElement(RuleConstants.SedalenKathisma1Node)]
        public YmnosStructure SedalenKathisma1 { get; set; }
        /// <summary>
        /// седален по 2-ой кафизме
        /// </summary>
        [XmlElement(RuleConstants.SedalenKathisma2Node)]
        public YmnosStructure SedalenKathisma2 { get; set; }
        /// <summary>
        /// седален по 3-ой кафизме
        /// </summary>
        [XmlElement(RuleConstants.SedalenKathisma3Node)]
        public YmnosStructure SedalenKathisma3 { get; set; }
        /// <summary>
        /// седален по полиелее
        /// </summary>
        [XmlElement(RuleConstants.SedalenPolyeleosNode)]
        public YmnosStructure SedalenPolyeleos { get; set; }
        /// <summary>
        /// Величания
        /// </summary>
        [XmlElement(RuleConstants.MegalynarionNode)]
        public ItemTextCollection Megalynarion { get; set; }
        /// <summary>
        /// Псалом избранный
        /// </summary>
        [XmlElement(RuleConstants.EclogarionNode)]
        public ItemTextCollection Eclogarion { get; set; }
        /// <summary>
        /// Прокимен на полиелее
        /// </summary>
        [XmlElement(RuleConstants.ProkeimenonNode)]
        public Prokeimenon Prokeimenon { get; set; }
        /// <summary>
        /// Евангельское чтение
        /// </summary>
        [XmlArray(RuleConstants.EvangelionNode)]
        [XmlArrayItem(RuleConstants.EvangelionPartNode, Type = typeof(EvangelionPart))]
        public List<EvangelionPart> Evangelion { get; set; }
        /// <summary>
        /// Стихира по 50-м псалме
        /// </summary>
        [XmlElement(RuleConstants.Sticheron50Node)]
        public YmnosGroup Sticheron50 { get; set; }
        /// <summary>
        /// Канон
        /// </summary>
        [XmlArray(RuleConstants.KanonesNode)]
        [XmlArrayItem(RuleConstants.KanonasNode)]
        public List<Kanonas> Kanones { get; set; }
        /// <summary>
        /// Стихиры на Хвалитех
        /// </summary>
        [XmlElement(RuleConstants.AinoiNode)]
        public YmnosStructure Ainoi { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        [XmlElement(RuleConstants.ApostichaNode)]
        public YmnosStructure Aposticha { get; set; }

        #endregion

        protected override void Validate()
        {
            if (SedalenKathisma1?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathisma1, /*ElementName + "." + */RuleConstants.SedalenKathisma1Node);
            }

            if (SedalenKathisma2?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathisma2, /*ElementName + "." + */RuleConstants.SedalenKathisma2Node);
            }

            if (SedalenKathisma3?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathisma3, /*ElementName + "." + */RuleConstants.SedalenKathisma3Node);
            }

            if (SedalenPolyeleos?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenPolyeleos, /*ElementName + "." + */RuleConstants.SedalenPolyeleosNode);
            }

            if (Megalynarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Megalynarion, /*ElementName + "." + */RuleConstants.MegalynarionNode);
            }

            if (Eclogarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Eclogarion, /*ElementName + "." + */RuleConstants.EclogarionNode);
            }

            if (Prokeimenon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prokeimenon, /*ElementName + "." + */RuleConstants.ProkeimenonNode);
            }

            if (Evangelion != null)
            {
                foreach (EvangelionPart part in Evangelion)
                {
                    if (!part.IsValid)
                    {
                        AppendAllBrokenConstraints(part, /*ElementName + "." + */RuleConstants.EvangelionNode);
                    }
                }
            }

            if (Sticheron50?.IsValid == false)
            {
                AppendAllBrokenConstraints(Sticheron50, /*ElementName + "." + */RuleConstants.Sticheron50Node);
            }

            if (Kanones != null)
            {
                foreach (Kanonas kanonas in Kanones)
                {
                    if (!kanonas.IsValid)
                    {
                        AppendAllBrokenConstraints(kanonas, /*ElementName + "." + */RuleConstants.KanonasNode);
                    }
                }
            }

            if (Ainoi?.IsValid == false)
            {
                AppendAllBrokenConstraints(Ainoi, /*ElementName + "." + */RuleConstants.AinoiNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, /*ElementName + "." + */RuleConstants.ApostichaNode);
            }
        }
    }
}