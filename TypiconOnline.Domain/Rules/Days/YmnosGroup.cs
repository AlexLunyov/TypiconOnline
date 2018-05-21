using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Days
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
        [XmlAttribute(RuleConstants.IhosAttrName)]
        public int Ihos { get; set; }

        /// <summary>
        /// Тип группы
        /// </summary>
        [XmlAttribute(RuleConstants.YmnosGroupKindAttrName)]
        [DefaultValue(YmnosGroupKind.Undefined)]
        public YmnosGroupKind Kind { get; set; }
        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        [XmlElement(RuleConstants.ProsomoionNode)]
        public Prosomoion Prosomoion { get; set; }

        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        [XmlElement(RuleConstants.AnnotationNode)]
        public ItemText Annotation { get; set; }

        private List<Ymnos> _ymnis = new List<Ymnos>();
        /// <summary>
        /// Коллекция песнопений
        /// </summary>
        [XmlElement(RuleConstants.YmnosNode)]
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
                AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, RuleConstants.ProkeimenonNode);
            }

            if (Prosomoion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prosomoion, RuleConstants.ProsomoionNode);
            }

            if (Annotation?.IsValid == false)
            {
                AppendAllBrokenConstraints(Annotation, RuleConstants.AnnotationNode);
            }

            foreach (Ymnos ymnos in Ymnis)
            {
                if (!ymnos.IsValid)
                {
                    AppendAllBrokenConstraints(ymnos, RuleConstants.YmnosNode);
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
