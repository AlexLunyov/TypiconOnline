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
        public YmnosGroup(XmlNode node) : base(node)
        {
            ////глас
            //XmlAttribute ihosAttr = node.Attributes[RuleConstants.YmnosIhosAttrName];
            //Ihos = (ihosAttr != null) ? new ItemInt(ihosAttr.Value) : new ItemInt();

            //подобен
            XmlNode prosomoionNode = node.SelectSingleNode(RuleConstants.ProsomoionNode);
            if (prosomoionNode != null)
            {
                Prosomoion = new Prosomoion(prosomoionNode);
            }

            //доп описание
            XmlNode annotationNode = node.SelectSingleNode(RuleConstants.AnnotationNode);
            if (annotationNode != null)
            {
                Annotation = new ItemText(annotationNode.OuterXml);
            }

            //песнопения
            Ymnis = new List<Ymnos>();
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

        /// <summary>
        /// Коллекция песнопений
        /// </summary>
        public List<Ymnos> Ymnis { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            //if (!Ihos.IsValid)
            //{
            //    AppendAllBrokenConstraints(Ihos, ElementName + "." + RuleConstants.YmnosIhosAttrName);
            //}
            //else if (!Ihos.IsEmpty)
            //{
            //    //глас должен иметь значения с 1 до 8
            //    if ((Ihos.Value < 1) || (Ihos.Value > 8))
            //    {
            //        AddBrokenConstraint(YmnosGroupBusinessConstraint.InvalidIhos, ElementName);
            //    }
            //}

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
    }
}
