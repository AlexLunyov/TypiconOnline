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
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для составления канонов
    /// </summary>
    public class KanonasRule : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        public KanonasRule(string name) : base(name) { }

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

        private List<Kanonas> _kanonesCalc;

        /// <summary>
        /// Вычисленные каноны правила
        /// </summary>
        public IEnumerable<Kanonas> KanonesCalculated
        {
            get
            {
                return _kanonesCalc.AsEnumerable();
            }
        }
        /// <summary>
        /// Седален по 3-й песне
        /// </summary>
        public YmnosStructure SedalenCalculated { get; private set; }
        /// <summary>
        /// Кондак по 6-ой песне
        /// </summary>
        public Kontakion KontakionCalculated { get; private set; }
        /// <summary>
        /// Эксапостиларий по 9-ой песне
        /// </summary>
        public Exapostilarion ExapostilarionCalculated { get; private set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasRule>())
            {
                _kanonesCalc = new List<Kanonas>();

                //используем специальный обработчик для KKatavasiaRule
                CollectorRuleHandler<KKatavasiaRule> katavasiaHandler = new CollectorRuleHandler<KKatavasiaRule>() { Settings = handler.Settings };

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, katavasiaHandler);
                }

                ExecContainer katavasiaContainer = katavasiaHandler.GetResult();

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
                    CalculateOdesStructure(date, handler, container, (katavasiaContainer != null));
                }

                if (katavasiaContainer != null)
                {
                    CalculateKatavasiaStructure(date, handler, katavasiaContainer);
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

        /// <summary>
        /// Добавляет в конец коллекции вычисляемых канонов катавасию
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        /// <param name="container"></param>
        private void CalculateKatavasiaStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            if (container?.ChildElements.FirstOrDefault() is KKatavasiaRule item)
            {
                _kanonesCalc.Add(item.Calculate(date, handler.Settings) as Kanonas);
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

            SedalenCalculated = sedalen;
        }

        private void CalculateKontakionStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            if (container?.ChildElements.FirstOrDefault() is KKontakionRule item)
            {
                KontakionCalculated = item.Calculate(date, handler.Settings) as Kontakion;
            }
        }

        private void CalculateOdesStructure(DateTime date, IRuleHandler handler, ExecContainer container, bool katavasiaExists)
        {
            for (int i = 0; i < container.ChildElements.Count; i++)
            {
                KanonasItem item = container.ChildElements[i] as KanonasItem;

                //определение катавасии отсутствует и канон последний
                item.IncludeKatavasia = (!katavasiaExists && i == container.ChildElements.Count - 1);

                if (item.Calculate(date, handler.Settings) is Kanonas k)
                {
                    _kanonesCalc.Add(k);
                }
            }
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
