using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для использования седальнов 
    /// </summary>
    public class KSedalenRule : KKontakionRule, ICalcStructureElement//<YmnosStructure>
    {
        public KSedalenRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.KSedalenPlaceAttrName];
            Place = (attr != null) ? new ItemEnumType<KanonasPlaceKind>(attr.Value) : null;
        }

        #region Properties

        /// <summary>
        /// Место в тексте богослужения для выбора канона
        /// </summary>
        public ItemEnumType<KanonasPlaceKind> Place { get; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KSedalenRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (Place == null)
            {
                AddBrokenConstraint(KSedalenRuleBusinessConstraint.PlaceRequired, ElementName);
            }
            else if (!Place.IsValid)
            {
                AppendAllBrokenConstraints(Place);
            }
        }

        public override DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            Kanonas kanonas = GetKanonas(settings);

            if (kanonas != null)
            {
                switch (Place.Value)
                {
                    case KanonasPlaceKind.sedalen:
                        if (kanonas.Sedalen != null)
                        {
                            result = new YmnosStructure();
                            result.Groups.AddRange(kanonas.Sedalen.Groups);
                        }
                        break;
                    case KanonasPlaceKind.kontakion:
                        if (kanonas.Kontakion != null)
                        {
                            //конвертируем структуру кондака в YmnosGroup
                            YmnosGroup group = new YmnosGroup()
                            {
                                Annotation = new ItemText(kanonas.Kontakion.Annotation),
                                Ihos = kanonas.Kontakion.Ihos,
                                Prosomoion = new Prosomoion(kanonas.Kontakion.Prosomoion),
                                Ymnis = new List<Ymnos>()
                                {
                                    new Ymnos() { Text = kanonas.Kontakion.Ymnos },
                                    new Ymnos() { Text = kanonas.Kontakion.Ikos }
                                }
                            };

                            result = new YmnosStructure();
                            result.Groups.Add(group);
                        }
                        break;
                }
            }

            return result;
        }

        
    }
}
