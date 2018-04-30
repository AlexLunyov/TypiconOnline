using System;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для составления канонов
    /// </summary>
    public class KanonasRule : IncludingRulesElement, ICustomInterpreted, IViewModelElement, IAsAdditionElement
    {
        public KanonasRule(string name, IRuleSerializerRoot serializerRoot,
            IElementViewModelFactory<KanonasRule> viewModelFactory, IAsAdditionElement parent) : base(name, serializerRoot)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in KanonasRuleBase");

            Parent = parent;
        }

        #region Properties

        /// <summary>
        /// Признак, является ли данное правило Каноном на утрене.
        /// В этом случае добавляется в 8-й песне вместо «Слава» - «Благословим», 
        /// а затем перед катавасией – «Хвалим, благословим…», 
        /// а также если явно не определена катавасия, то добавляется после 3, 6, 8, 9 песен
        /// </summary>
        public bool IsOrthros { get; set; }

        //IReadOnlyList<KAfterRule> _afterRules = null;
        /// <summary>
        /// Коллекция дочерних элементов, описывающих правила после n-ой песни канона
        /// </summary>
        public IReadOnlyList<KAfterRule> AfterRules { get; private set; } = new List<KAfterRule>();
        //{
        //    get
        //    {
        //        if (_afterRules == null)
        //        {
        //            //находим KAfterRules
        //            _afterRules = GetChildElements<KAfterRule>();
        //        }

        //        return _afterRules;
        //    }
        //    private set
        //    {
        //        _afterRules = value;
        //    }
        //}

        //private IReadOnlyList<KOdiRule> _odes = null;
        /// <summary>
        /// Песни канона, где каждая отдельно определяет последовательность тропарей
        /// </summary>
        public IReadOnlyList<KOdiRule> Odes { get; private set; } = new List<KOdiRule>();
        //{
        //    get
        //    {
        //        if (_odes == null)
        //        {
        //            //находим Odes
        //            _odes = GetChildElements<KOdiRule>();
        //        }

        //        return _odes;
        //    }
        //    private set
        //    {
        //        _odes = value;
        //    }
        //}

        protected IElementViewModelFactory<KanonasRule> ViewModelFactory { get; }

        #region IRewritableElement implementation

        public IAsAdditionElement Parent { get; }

        public string AsAdditionName
        {
            get
            {
                string result = ElementName;

                if (Parent != null)
                {
                    result = $"{Parent.AsAdditionName}/{result}";
                }

                return result;
            }
        }

        public AsAdditionMode AsAdditionMode { get; set; }

        public void RewriteValues(IAsAdditionElement source)
        {
            if (source is KanonasRule s)
            {
                IsOrthros = s.IsOrthros;
            }
        }

        #endregion

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasRule>() && !this.AsAdditionHandled(handler))
            {
                //Добавляем IAsAdditionElement append реализацию
                //Приходится явно вызывать метод, т.к. функционал ExecContainer.InnerInterpret не используется 
                AppendHandling(handler);

                Odes = GetChildElements<KOdiRule>(handler.Settings);

                AfterRules = GetChildElements<KAfterRule>(handler.Settings);

                foreach (var ode in Odes)
                {
                    ode.Interpret(handler);
                }

                foreach (var afterRule in AfterRules)
                {
                    afterRule.Interpret(handler);
                }

                handler.Execute(this);
            }
        }

        //protected override void Validate()
        //{
        //    base.Validate();

        //    if (Odes.Count == 0)
        //    {
        //        AddBrokenConstraint(KanonasRuleBusinessConstraint.OdiRequired);
        //    }

        //    //ищем KOdi с одинаковым номером
        //    var sameOdes = Odes.Where(str => Odes.Count(s => s.Number == str.Number) > 1);
        //    if (sameOdes?.Count() > 0)
        //    {
        //        AddBrokenConstraint(KanonasRuleBusinessConstraint.OdesWithSameNumber);
        //    }

        //    //ищем KAfterRule с одинаковым номером
        //    var sameAfterRules = AfterRules.Where(str => AfterRules.Count(s => s.OdiNumber == str.OdiNumber) > 1);
        //    if (sameAfterRules?.Count() > 0)
        //    {
        //        AddBrokenConstraint(KanonasRuleBusinessConstraint.AfterRulesWithSameNumber);
        //    }
        //}

        ///// <summary>
        ///// Добавляет в конец коллекции вычисляемых канонов катавасию
        ///// </summary>
        ///// <param name="handler"></param>
        ///// <param name="container"></param>
        //private void CalculateKatavasiaStructure(IRuleHandler handler, IReadOnlyList<KKatavasiaRule> container)
        //{
        //    if (container.FirstOrDefault() is KKatavasiaRule item)
        //    {
        //        _kanonesCalc.Add(item.Calculate(handler.Settings) as Kanonas);
        //    }
        //}


        //private void CalculateOdesStructure(IRuleHandler handler, IReadOnlyList<KKanonasItemRule> container, bool katavasiaExists)
        //{
        //    for (int i = 0; i < container.Count; i++)
        //    {
        //        KKanonasItemRule item = container[i] as KKanonasItemRule;

        //        if (item.Calculate(handler.Settings) is Kanonas k)
        //        {
        //            _kanonesCalc.Add(k);
        //        }

        //        //это правило для канона Утрени, определение катавасии отсутствует и канон последний
        //        if (IsOrthros && !katavasiaExists && i == container.Count - 1)
        //        {
        //            //добавляем еще один канон, который будет состоять ТОЛЬКО из катавасий после 3, 6, 8, 9-х песен
        //            if (item.CalculateEveryDayKatavasia(handler.Settings) is Kanonas k1)
        //            {
        //                _kanonesCalc.Add(k1);
        //            }
        //        }
        //    }
        //}

        public void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<KanonasRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }      
    }

    public class KanonasRuleBusinessConstraint
    {
        public static readonly BusinessConstraint OdiRequired 
            = new BusinessConstraint($"В дочерних элементах должен быть определенхотя бы один элемент {RuleConstants.KOdiRuleNode}.");

        public static readonly BusinessConstraint OdesWithSameNumber
            = new BusinessConstraint($"Определено более одного элемента {RuleConstants.KOdiRuleNode} с одинаковым номером песни.");

        public static readonly BusinessConstraint AfterRulesWithSameNumber
            = new BusinessConstraint($"Определено более одного элемента {RuleConstants.KAfterNode} с одинаковым номером песни.");
    }
}
