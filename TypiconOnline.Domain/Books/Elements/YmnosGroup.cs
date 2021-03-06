﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Books.Elements
{
    /// <summary>
    /// Песнопения, сгруппированные по подобнам
    /// </summary>
    [Serializable]
    public class YmnosGroup : DayElementBase, IContainingIhos
    {
        public YmnosGroup()
        {
            //Prosomoion = new Prosomoion();
            //Annotation = new ItemText();
        }

        public YmnosGroup(YmnosGroup source) 
        {
            CloneValues(source);

            source.Ymnis.ForEach(c => Ymnis.Add(new Ymnos(c)));
        }

        #region Properties

        /// <summary>
        /// Глас
        /// </summary>
        [XmlAttribute(ElementConstants.IhosAttrName)]
        public int Ihos { get; set; }

        /// <summary>
        /// Тип группы
        /// </summary>
        [XmlAttribute(ElementConstants.YmnosGroupKindAttrName)]
        [DefaultValue(YmnosGroupKind.Undefined)]
        public YmnosGroupKind Kind { get; set; }
        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        [XmlElement(ElementConstants.ProsomoionNode)]
        public Prosomoion Prosomoion { get; set; }

        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(ElementConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }

        private List<Ymnos> _ymnis = new List<Ymnos>();
        /// <summary>
        /// Коллекция песнопений
        /// </summary>
        [XmlElement(ElementConstants.YmnosNode)]
        public List<Ymnos> Ymnis
        {
            get
            {
                return _ymnis;
            }
            set
            {
                _ymnis = value;
            }
        }

        #endregion

        protected override void Validate()
        {
            //глас должен иметь значения с 1 до 8
            if ((Ihos < 1) || (Ihos > 8))
            {
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, ElementConstants.ProkeimenonNode);
            }

            if (Prosomoion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prosomoion, ElementConstants.ProsomoionNode);
            }

            if (Annotation?.IsValid == false)
            {
                AppendAllBrokenConstraints(Annotation, ElementConstants.AnnotationNode);
            }

            foreach (Ymnos ymnos in Ymnis)
            {
                if (!ymnos.IsValid)
                {
                    AppendAllBrokenConstraints(ymnos, ElementConstants.YmnosNode);
                }
            }
        }

        public bool Equals(YmnosGroup ymnosGroup)
        {
            if (ymnosGroup == null) throw new ArgumentNullException("YmnosGroup.Equals");

            return (Ihos.Equals(ymnosGroup.Ihos)
                && Annotation == ymnosGroup.Annotation
                && Prosomoion == ymnosGroup.Prosomoion);
                //&& Annotation?.Equals(ymnosGroup.Annotation) == true 
                //&& Prosomoion?.Equals(ymnosGroup.Prosomoion) == true);
        }

        private void CloneValues(YmnosGroup source)
        {
            if (source.Prosomoion != null)
            {
                Prosomoion = new Prosomoion(source.Prosomoion);
            }

            if (source.Annotation != null)
            {
                Annotation = new ItemText(source.Annotation);
            }

            Ihos = source.Ihos;
        }

        /// <summary>
        /// Возвращает группу с параметрами родителя и с одним песнопением по указанному индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public YmnosGroup GetGroupWithSingleYmnos(int index)
        {
            if (index < 0 || index >= Ymnis.Count)
            {
                throw new IndexOutOfRangeException("GetGroupWithSingleYmnos");
            }

            var result = new YmnosGroup();

            result.CloneValues(this);
            result.Ymnis.Add(new Ymnos(Ymnis[index]));

            return result;
        }
    }
}
