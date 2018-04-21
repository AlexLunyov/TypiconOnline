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
    public class EktenisRule : ExecContainer, ICustomInterpreted//, IViewModelElement
    {
        public EktenisRule(string name) : base(name) { } 

        //public ItemText Name { get; set; }

        //public List<TextHolder> CalculatedElements { get; private set; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<EktenisRule>())
            {
                base.InnerInterpret(handler);
                //используем специальный обработчик для Ektenis,
                //чтобы создать вычисленный список элементов TextHolder
                //ExecContainer container = GetChildElements<TextHolder>(date, handler);

                //CalculatedElements = container?.ChildElements.Cast<TextHolder>().ToList();

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            //if (Name == null || !Name.IsValid || Name.IsEmpty)
            //{
            //    AddBrokenConstraint(EktenisBusinessConstraint.NameReqiured);
            //}

            base.Validate();

            //foreach (RuleElement element in ChildElements)
            //{
            //    if (element == null)
            //    {
            //        AddBrokenConstraint(ExecContainerBusinessConstraint.InvalidChild);
            //    }
            //    //добавляем ломаные правила к родителю
            //    else if (!element.IsValid)
            //    {
            //        AppendAllBrokenConstraints(element);
            //    }
            //}
        }
    }
}
