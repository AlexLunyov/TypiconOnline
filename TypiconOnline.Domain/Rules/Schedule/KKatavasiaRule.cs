using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Katavasia;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Приавло для использования катавасии в каноне
    /// </summary>
    public class KKatavasiaRule : KKontakionRule, ICustomInterpreted, ICalcStructureElement
    {
        IKatavasiaContext katavasiaContext;

        public KKatavasiaRule(string name, IKatavasiaContext context) : base(name)
        {
            katavasiaContext = context ?? throw new ArgumentNullException("IKatavasiaContext");
        }

        #region Properties

        private Kanonas _katavasia;

        public string Name { get; set; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KKatavasiaRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            //если name не определено, то тогда используем валидацию базового класса
            if (string.IsNullOrEmpty(Name))
            {
                base.Validate();
            }
            //находим в хранилище
            else if (GetFromRepository() == null)
            {
                AddBrokenConstraint(KKatavasiaRuleBusinessConstraint.InvalidName, ElementName);
            }

            //двойное определение
            if (!string.IsNullOrEmpty(Name)
                && (Source != null || Kanonas != null))
            {
                AddBrokenConstraint(KKatavasiaRuleBusinessConstraint.DoubleDefinition, ElementName);
            }
        }

        public override DayElementBase Calculate(DateTime date, RuleHandlerSettings settings)
        {
            return (string.IsNullOrEmpty(Name)) ? GetFromSource(settings) : GetFromRepository();
        }

        private Kanonas GetFromRepository()
        {
            if (_katavasia == null)
            {
                GetKatavasiaResponse response = katavasiaContext.Get(
                    new GetKatavasiaRequest() { Name = Name });

                if (response.Exception == null && response.BookElement != null)
                {
                    _katavasia = response.BookElement;
                }
            }

            return _katavasia;
        }

        private Kanonas GetFromSource(RuleHandlerSettings settings)
        {
            Kanonas result = null;
            Kanonas source = GetKanonas(settings);

            if (source != null)
            {
                result = new Kanonas()
                {
                    Acrostic = source.Acrostic,
                    Annotation = source.Annotation,
                    Ihos = source.Ihos,
                    Stihos = source.Stihos
                };

                foreach (Odi odi in source.Odes)
                {
                    Odi o = new Odi() { Number = odi.Number };

                    //добавляем катавасию(и)
                    o.Troparia.AddRange(odi.Troparia.FindAll(c => c.Kind == YmnosKind.Katavasia));
                    //добавляем саму песнь
                    result.Odes.Add(o);
                }
            }

            return result;
        }
    }
}
