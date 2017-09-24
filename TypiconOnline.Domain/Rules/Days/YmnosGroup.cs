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
    public class YmnosGroup : ValueObjectBase, IContainingIhos
    {
        public YmnosGroup()
        {
            //Prosomoion = new Prosomoion();
            //Annotation = new ItemText();
        }

        public YmnosGroup(YmnosGroup source) 
        {
            Prosomoion = new Prosomoion(source.Prosomoion);
            Annotation = new ItemText(source.Annotation.StringExpression);

            Ihos = source.Ihos;

            source.Ymnis.ForEach(c => Ymnis.Add(new Ymnos(c)));
        }

        public YmnosGroup(XmlNode node) 
        {
            ////глас
            //XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            //Ihos = (ihosAttr != null) ? new ItemInt(ihosAttr.Value) : new ItemInt();

            //глас
            XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            if (ihosAttr != null)
            {
                int result = default(int);
                int.TryParse(ihosAttr.Value, out result);
                Ihos = result;
            }

            //подобен
            XmlNode prosomoionNode = node.SelectSingleNode(RuleConstants.ProsomoionNode);
            Prosomoion = (prosomoionNode != null) ? new Prosomoion(prosomoionNode) : new Prosomoion();

            //доп описание
            XmlNode annotationNode = node.SelectSingleNode(RuleConstants.AnnotationNode);
            Annotation = (annotationNode != null) ? new ItemText(annotationNode.OuterXml) : new ItemText();

            //песнопения
            XmlNodeList ymnisList = node.SelectNodes(RuleConstants.YmnosNode);
            if (ymnisList != null)
            {
                foreach (XmlNode ymnosItemNode in ymnisList)
                {
                    Ymnis.Add(new Ymnos(ymnosItemNode));
                }
            }
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
                && Annotation?.Equals(ymnosGroup.Annotation) == true 
                && Prosomoion?.Equals(ymnosGroup.Prosomoion) == true);
        }
    }
}
