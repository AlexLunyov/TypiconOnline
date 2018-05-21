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
    [XmlRoot(RuleConstants.OrthrosNode)]
    public class Orthros : DayElementBase
    {
        public Orthros() { }

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
        /// Степенны
        /// </summary>
        [XmlElement(RuleConstants.AnavathmosNode)]
        public YmnosStructure Anavathmos { get; set; }
        /// <summary>
        /// Величания
        /// </summary>
        [XmlArray(RuleConstants.MegalynarionNode)]
        [XmlArrayItem(RuleConstants.YmnosStihosNode)]
        public List<ItemText> Megalynarion { get; set; }
        /// <summary>
        /// Псалом избранный
        /// </summary>
        [XmlArray(RuleConstants.EclogarionNode)]
        [XmlArrayItem(RuleConstants.YmnosStihosNode)]
        public List<ItemText> Eclogarion { get; set; }
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
        /// седален по 3-ой песне
        /// </summary>
        [XmlElement(RuleConstants.KanonasSedalenNode)]
        public YmnosStructure SedalenKanonas { get; set; }
        /// <summary>
        /// кондак по 6-ой песне
        /// </summary>
        [XmlArray(RuleConstants.KontakiaNode)]
        [XmlArrayItem(RuleConstants.KontakionNode)]
        public List<Kontakion> Kontakia { get; set; }
        /// <summary>
        /// Эксапостиларий по 9-й песне
        /// </summary>
        [XmlElement(RuleConstants.KanonasExapostilarionNode)]
        public Exapostilarion Exapostilarion { get; set; }
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

            if (Anavathmos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Anavathmos, /*ElementName + "." + */RuleConstants.AnavathmosNode);
            }

            if (Megalynarion != null)
            {
                foreach (ItemText item in Megalynarion)
                {
                    if (!item.IsValid)
                    {
                        AppendAllBrokenConstraints(item, RuleConstants.MegalynarionNode);
                    }
                }
            }

            if (Eclogarion != null)
            {
                foreach (ItemText item in Eclogarion)
                {
                    if (!item.IsValid)
                    {
                        AppendAllBrokenConstraints(item, RuleConstants.EclogarionNode);
                    }
                }
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

            if (SedalenKanonas?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKanonas, RuleConstants.KanonasSedalenNode);
            }

            if (Kontakia != null)
            {
                foreach (var element in Kontakia)
                {
                    if (!element.IsValid)
                    {
                        AppendAllBrokenConstraints(element, /*ElementName + "." + */RuleConstants.KontakionNode);
                    }
                }
            }
    
            if (Exapostilarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Exapostilarion, RuleConstants.KanonasExapostilarionNode);
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