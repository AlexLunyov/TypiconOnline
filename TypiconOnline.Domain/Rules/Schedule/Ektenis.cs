using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для ектений
    /// </summary>
    public class Ektenis : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        private List<TextHolder> _calculatedText;

        public Ektenis(XmlNode node) : base(node) { }

        //public Ektenis(XmlNode node) : base(node)
        //{
        //    ChildElements = new List<RuleElement>();

        //    if (node.HasChildNodes)
        //    {
        //        foreach (XmlNode childNode in node.ChildNodes)
        //        {
        //            if (childNode.Name == RuleConstants.EktenisNameNode)
        //            {
        //                Name = new ItemText(childNode.OuterXml);
        //            }
        //            else
        //            {
        //                RuleElement element = Factories.RuleFactory.CreateElement(childNode);
        //                ChildElements.Add(element);
        //            }
        //        }
        //    }
        //}

        //public ItemText Name { get; set; }

        //public virtual List<RuleElement> ChildElements { get; set; }

        public List<TextHolder> CalculatedElements
        {
            get
            {
                return _calculatedText;
            }
        }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<Ektenis>())
            {
                //используем специальный обработчик для Ektenis,
                //чтобы создать вычисленный список элементов TextHolder
                CollectorRuleHandler<TextHolder> structHandler = new CollectorRuleHandler<TextHolder>() { Settings = handler.Settings };

                foreach (RuleElement elem in ChildElements)
                {
                    elem.Interpret(date, structHandler);
                }

                ExecContainer container = structHandler.GetResult();

                _calculatedText = container?.ChildElements.Cast<TextHolder>().ToList();
            }
        }

        protected override void Validate()
        {
            //if (Name == null || !Name.IsValid || Name.IsEmpty)
            //{
            //    AddBrokenConstraint(EktenisBusinessConstraint.NameReqiured);
            //}

            foreach (RuleElement element in ChildElements)
            {
                if (element == null)
                {
                    AddBrokenConstraint(ExecContainerBusinessConstraint.InvalidChild);
                }
                //добавляем ломаные правила к родителю
                else if (!element.IsValid)
                {
                    AppendAllBrokenConstraints(element);
                }
            }
        }

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new EktenisViewModel(this, handler);
        }
    }
}
