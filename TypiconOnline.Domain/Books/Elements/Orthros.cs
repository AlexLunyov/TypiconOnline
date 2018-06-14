using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    [Serializable]
    [XmlRoot(ElementConstants.OrthrosNode)]
    public class Orthros : DayElementBase
    {
        public Orthros() { }

        #region Properties

        /// <summary>
        /// седален по 1-ой кафизме
        /// </summary>
        [XmlElement(ElementConstants.SedalenKathisma1Node)]
        public YmnosStructure SedalenKathisma1 { get; set; }
        /// <summary>
        /// седален по 2-ой кафизме
        /// </summary>
        [XmlElement(ElementConstants.SedalenKathisma2Node)]
        public YmnosStructure SedalenKathisma2 { get; set; }
        /// <summary>
        /// седален по 3-ой кафизме
        /// </summary>
        [XmlElement(ElementConstants.SedalenKathisma3Node)]
        public YmnosStructure SedalenKathisma3 { get; set; }
        /// <summary>
        /// седален по полиелее
        /// </summary>
        [XmlElement(ElementConstants.SedalenPolyeleosNode)]
        public YmnosStructure SedalenPolyeleos { get; set; }

        /// <summary>
        /// Степенны
        /// </summary>
        [XmlElement(ElementConstants.AnavathmosNode)]
        public YmnosStructure Anavathmos { get; set; }
        /// <summary>
        /// Величания
        /// </summary>
        [XmlArray(ElementConstants.MegalynarionNode)]
        [XmlArrayItem(ElementConstants.YmnosStihosNode)]
        public List<ItemText> Megalynarion { get; set; }
        /// <summary>
        /// Псалом избранный
        /// </summary>
        [XmlArray(ElementConstants.EclogarionNode)]
        [XmlArrayItem(ElementConstants.YmnosStihosNode)]
        public List<ItemText> Eclogarion { get; set; }
        /// <summary>
        /// Прокимен на полиелее
        /// </summary>
        [XmlElement(ElementConstants.ProkeimenonNode)]
        public Prokeimenon Prokeimenon { get; set; }
        /// <summary>
        /// Евангельское чтение
        /// </summary>
        [XmlArray(ElementConstants.EvangelionNode)]
        [XmlArrayItem(ElementConstants.EvangelionPartNode, Type = typeof(EvangelionPart))]
        public List<EvangelionPart> Evangelion { get; set; }
        /// <summary>
        /// Стихира по 50-м псалме
        /// </summary>
        [XmlElement(ElementConstants.Sticheron50Node)]
        public YmnosGroup Sticheron50 { get; set; }
        /// <summary>
        /// Канон
        /// </summary>
        [XmlArray(ElementConstants.KanonesNode)]
        [XmlArrayItem(ElementConstants.KanonasNode)]
        public List<Kanonas> Kanones { get; set; }
        /// <summary>
        /// седален по 3-ой песне
        /// </summary>
        [XmlElement(ElementConstants.KanonasSedalenNode)]
        public YmnosStructure SedalenKanonas { get; set; }
        /// <summary>
        /// кондак по 6-ой песне
        /// </summary>
        [XmlArray(ElementConstants.KontakiaNode)]
        [XmlArrayItem(ElementConstants.KontakionNode)]
        public List<Kontakion> Kontakia { get; set; }
        /// <summary>
        /// Эксапостиларий по 9-й песне
        /// </summary>
        [XmlElement(ElementConstants.ExapostilarionNode)]
        public Exapostilarion Exapostilarion { get; set; }
        /// <summary>
        /// Стихиры на Хвалитех
        /// </summary>
        [XmlElement(ElementConstants.AinoiNode)]
        public YmnosStructure Ainoi { get; set; }
        /// <summary>
        /// Стихиры на стиховне
        /// </summary>
        [XmlElement(ElementConstants.ApostichaNode)]
        public YmnosStructure Aposticha { get; set; }

        #endregion

        protected override void Validate()
        {
            if (SedalenKathisma1?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathisma1, /*ElementName + "." + */ElementConstants.SedalenKathisma1Node);
            }

            if (SedalenKathisma2?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathisma2, /*ElementName + "." + */ElementConstants.SedalenKathisma2Node);
            }

            if (SedalenKathisma3?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKathisma3, /*ElementName + "." + */ElementConstants.SedalenKathisma3Node);
            }

            if (SedalenPolyeleos?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenPolyeleos, /*ElementName + "." + */ElementConstants.SedalenPolyeleosNode);
            }

            if (Anavathmos?.IsValid == false)
            {
                AppendAllBrokenConstraints(Anavathmos, /*ElementName + "." + */ElementConstants.AnavathmosNode);
            }

            if (Megalynarion != null)
            {
                foreach (ItemText item in Megalynarion)
                {
                    if (!item.IsValid)
                    {
                        AppendAllBrokenConstraints(item, ElementConstants.MegalynarionNode);
                    }
                }
            }

            if (Eclogarion != null)
            {
                foreach (ItemText item in Eclogarion)
                {
                    if (!item.IsValid)
                    {
                        AppendAllBrokenConstraints(item, ElementConstants.EclogarionNode);
                    }
                }
            }

            if (Prokeimenon?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prokeimenon, /*ElementName + "." + */ElementConstants.ProkeimenonNode);
            }

            if (Evangelion != null)
            {
                foreach (EvangelionPart part in Evangelion)
                {
                    if (!part.IsValid)
                    {
                        AppendAllBrokenConstraints(part, /*ElementName + "." + */ElementConstants.EvangelionNode);
                    }
                }
            }

            if (Sticheron50?.IsValid == false)
            {
                AppendAllBrokenConstraints(Sticheron50, /*ElementName + "." + */ElementConstants.Sticheron50Node);
            }

            if (Kanones != null)
            {
                foreach (Kanonas kanonas in Kanones)
                {
                    if (!kanonas.IsValid)
                    {
                        AppendAllBrokenConstraints(kanonas, /*ElementName + "." + */ElementConstants.KanonasNode);
                    }
                }
            }

            if (SedalenKanonas?.IsValid == false)
            {
                AppendAllBrokenConstraints(SedalenKanonas, ElementConstants.KanonasSedalenNode);
            }

            if (Kontakia != null)
            {
                foreach (var element in Kontakia)
                {
                    if (!element.IsValid)
                    {
                        AppendAllBrokenConstraints(element, /*ElementName + "." + */ElementConstants.KontakionNode);
                    }
                }
            }
    
            if (Exapostilarion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Exapostilarion, ElementConstants.ExapostilarionNode);
            }

            if (Ainoi?.IsValid == false)
            {
                AppendAllBrokenConstraints(Ainoi, /*ElementName + "." + */ElementConstants.AinoiNode);
            }

            if (Aposticha?.IsValid == false)
            {
                AppendAllBrokenConstraints(Aposticha, /*ElementName + "." + */ElementConstants.ApostichaNode);
            }
        }
    }
}