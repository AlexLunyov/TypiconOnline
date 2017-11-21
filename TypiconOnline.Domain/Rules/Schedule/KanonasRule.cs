using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для составления канонов
    /// </summary>
    public class KanonasRule : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        public KanonasRule(XmlNode node) : base(node)
        {
            XmlAttribute attr = node.Attributes[RuleConstants.KanonasRuleEktenis3AttrName];
            Ektenis3 = (attr != null) ? new CommonRuleElement(attr.Value) : null;

            attr = node.Attributes[RuleConstants.KanonasRuleEktenis6AttrName];
            Ektenis6 = (attr != null) ? new CommonRuleElement(attr.Value) : null;

            attr = node.Attributes[RuleConstants.KanonasRuleEktenis9AttrName];
            Ektenis9 = (attr != null) ? new CommonRuleElement(attr.Value) : null;

            attr = node.Attributes[RuleConstants.KanonasRulePanagiasAttrName];
            Panagias = (attr != null) ? new CommonRuleElement(attr.Value) : null;
        }

        #region Properties
        /// <summary>
        /// CommonRule описывающее ектению по 3-ей песне канона
        /// </summary>
        public CommonRuleElement Ektenis3 { get; set; }
        /// <summary>
        /// CommonRule описывающее ектению по 6-ей песне канона
        /// </summary>
        public CommonRuleElement Ektenis6 { get; set; }
        /// <summary>
        /// CommonRule описывающее ектению по 9-ей песне канона
        /// </summary>
        public CommonRuleElement Ektenis9 { get; set; }
        /// <summary>
        /// CommonRule описывающее Честнейшую
        /// </summary>
        public CommonRuleElement Panagias { get; set; }

        public Kanonas KanonasCalculated { get; private set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasRule>())
            {
                KanonasCalculated = new Kanonas();

                //используем специальный обработчик для KanonasItem,
                //чтобы создать список источников канонов на обработку
                CollectorRuleHandler<KanonasItem> kanonasHandler = new CollectorRuleHandler<KanonasItem>() { Settings = handler.Settings };

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, kanonasHandler);
                }

                ExecContainer container = kanonasHandler.GetResult();

                if (container != null)
                {
                    CalculateOdesStructure(date, handler, container);
                }

                //используем специальный обработчик для KSedalenRule
                CollectorRuleHandler<KSedalenRule> sedalenHandler = new CollectorRuleHandler<KSedalenRule>() { Settings = handler.Settings };

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, sedalenHandler);
                }

                container = sedalenHandler.GetResult();

                if (container != null)
                {
                    CalculateSedalenStructure(date, handler, container);
                }

                //используем специальный обработчик для KKontakionRule
                CollectorRuleHandler<KKontakionRule> kontakionHandler = new CollectorRuleHandler<KKontakionRule>() { Settings = handler.Settings };

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, kontakionHandler);
                }

                container = kontakionHandler.GetResult();

                if (container != null)
                {
                    CalculateKontakionStructure(date, handler, container);
                }

                //CommonRules
                Ektenis3?.Interpret(date, handler);
                Ektenis6?.Interpret(date, handler);
                Ektenis9?.Interpret(date, handler);
                Panagias?.Interpret(date, handler);

                handler.Execute(this);
            }
        }

        private void CalculateSedalenStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            YmnosStructure sedalen = new YmnosStructure();

            foreach (KSedalenRule item in container.ChildElements)
            {
                YmnosStructure s = item.Calculate(date, handler.Settings) as YmnosStructure;

                if (s != null)
                {
                    if (item is KSedalenTheotokionRule)
                    {
                        sedalen.Theotokion = s.Theotokion;
                    }
                    else
                    {
                        sedalen.Groups.AddRange(s.Groups);
                    }
                }
            }

            KanonasCalculated.Sedalen = sedalen;
        }

        private void CalculateKontakionStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            if (container.ChildElements.FirstOrDefault() is KKontakionRule item)
            {
                KanonasCalculated.Kontakion = item.Calculate(date, handler.Settings) as Kontakion;
            }
        }

        private void CalculateOdesStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            bool first = true;
            foreach (KanonasItem item in container.ChildElements)
            {
                if (item.Calculate(date, handler.Settings) is Kanonas k)
                {
                    if (first)
                    {
                        KanonasCalculated.Acrostic = k.Acrostic;
                        first = false;
                    }

                    foreach (Odi odi in k.Odes)
                    {
                        Odi currentOdi = KanonasCalculated.Odes.FirstOrDefault(c => c.Number == odi.Number);

                        if (currentOdi == null)
                        {
                            currentOdi = new Odi() { Number = odi.Number };
                            KanonasCalculated.Odes.Add(currentOdi);
                        }

                        currentOdi.Troparia.AddRange(odi.Troparia);
                    }
                }
            }
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
