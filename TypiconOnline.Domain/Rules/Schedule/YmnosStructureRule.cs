using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Базовый класс для описания правил стихир в последовательности богослужений
    /// </summary>
    public abstract class YmnosStructureRule : ExecContainer, ICustomInterpreted, IViewModelElement
    {
        public YmnosStructureRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, IRuleSerializerRoot serializer, string name) : base(name)
        {
            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in YmnosStructureRule");
        }

        #region Properties

        /// <summary>
        /// Тип структуры (Господи воззвах, стихиры на стиховне и т.д.)
        /// </summary>
        public YmnosStructureKind Kind { get; set; }

        /// <summary>
        /// Общее количество песнопений (ограничение)
        /// </summary>
        public int TotalYmnosCount { get; set; }

        /// <summary>
        /// Вычисленная последовательность богослужебных текстов
        /// </summary>
        public YmnosStructure Structure { get; private set; }

        public IRuleSerializerRoot Serializer { get; }

        protected IElementViewModelFactory<YmnosStructureRule> ViewModelFactory { get; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<YmnosStructureRule>())
            {
                //используем специальный обработчик для YmnosStructureRule,
                //чтобы создать список источников стихир на обработку
                var container = GetChildElements<ICalcStructureElement>(handler.Settings);

                if (container != null)
                {
                    CalculateYmnosStructure(handler.Settings, container);
                }

                handler.Execute(this);
            }
        }

        private void CalculateYmnosStructure(RuleHandlerSettings settings, IEnumerable<ICalcStructureElement> container)
        {
            Structure = new YmnosStructure();
            foreach (ICalcStructureElement element in container)
            {
                if (element.Calculate(settings) is YmnosStructure s)
                {
                    switch (element)
                    {
                        case YmnosRule r:
                            AsYmnosRule(r, s);
                            break;
                        case KSedalenRule r:
                            AsKanonasSedalenRule(r, s);
                            break;
                    }
                }
            }
        }

        private void AsYmnosRule(YmnosRule r, YmnosStructure s)
        {
            switch (r.Kind)
            {
                case YmnosRuleKind.YmnosRule:
                    Structure.Groups.AddRange(s.Groups);
                    break;
                case YmnosRuleKind.DoxastichonRule:
                    Structure.Doxastichon = s.Doxastichon;
                    break;
                case YmnosRuleKind.TheotokionRule:
                    Structure.Theotokion = s.Theotokion;
                    break;
            }
        }

        private void AsKanonasSedalenRule(KSedalenRule r, YmnosStructure s)
        {
            if (r is KSedalenTheotokionRule)
            {
                Structure.Theotokion = s.Theotokion;
            }
            else
            {
                Structure.Groups.AddRange(s.Groups);
            }
        }

        protected override void Validate()
        {
            base.Validate();
            //TODO: добавить проверку на наличие элементов stichira в дочерних элементах
        }

        public virtual void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<YmnosStructureRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }
    }
}
