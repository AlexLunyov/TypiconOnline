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
    /// Песнопения, сгруппированные по гласу и подобнам
    /// </summary>
    public class YmnosGroup : IhosRuleElement
    {
        public YmnosGroup()
        {
            Prosomoion = new Prosomoion();
            Annotation = new ItemText();
        }

        public YmnosGroup(YmnosGroup source) : base(source)
        {
            Prosomoion = new Prosomoion(source.Prosomoion);
            Annotation = new ItemText(source.Annotation.StringExpression);

            source.Ymnis.ForEach(c => Ymnis.Add(new Ymnos(c)));
        }

        public YmnosGroup(XmlNode node) : base(node)
        {
            ////глас
            //XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            //Ihos = (ihosAttr != null) ? new ItemInt(ihosAttr.Value) : new ItemInt();

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

        ///// <summary>
        ///// Глас
        ///// </summary>
        //public ItemInt Ihos { get; set; }

        /// <summary>
        /// Название подобна (самоподобна)
        /// </summary>
        public Prosomoion Prosomoion { get; set; }

        /// <summary>
        /// Дополнительные сведения. Например, "Феофаново" - Про стихиру
        /// </summary>
        public ItemText Annotation { get; set; }

        private List<Ymnos> _ymnis = new List<Ymnos>();
        /// <summary>
        /// Коллекция песнопений
        /// </summary>
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

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            base.Validate();

            if (Prosomoion?.IsValid == false)
            {
                AppendAllBrokenConstraints(Prosomoion, ElementName + "." + RuleConstants.ProsomoionNode);
            }

            if (Annotation?.IsValid == false)
            {
                AppendAllBrokenConstraints(Annotation, ElementName + "." + RuleConstants.AnnotationNode);
            }

            foreach (Ymnos ymnos in Ymnis)
            {
                if (!ymnos.IsValid)
                {
                    AppendAllBrokenConstraints(ymnos, ElementName + "." + RuleConstants.YmnosNode);
                }
            }
        }

        public bool Equals(YmnosGroup ymnosGroup)
        {
            if (ymnosGroup == null)
            {
                throw new ArgumentNullException("YmnosGroup.Equals");
            }
            return (Ihos.Equals(ymnosGroup.Ihos) && Annotation.Equals(ymnosGroup.Annotation) && Prosomoion.Equals(ymnosGroup.Prosomoion));
        }
    }
}
