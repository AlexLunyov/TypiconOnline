using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Абстрактный класс для Правил канонов
    /// </summary>
    public abstract class KanonasRuleBase : IncludingRulesElement, ICustomInterpreted, IViewModelElement
    {
        public KanonasRuleBase(string name, IRuleSerializerRoot serializerRoot) : base(name, serializerRoot) { }

        /// <summary>
        /// Признак, является ли данное правило Каноном на утрене.
        /// В этом случае добавляется в 8-й песне вместо «Слава» - «Благословим», 
        /// а затем перед катавасией – «Хвалим, благословим…», 
        /// а также если явно не определена катавасия, то добавляется после 3, 6, 8, 9 песен
        /// </summary>
        public bool IsOrthros { get; set; }

        /// <summary>
        /// Коллекция дочерних элементов, описывающих правила после n-ой песни канона
        /// </summary>
        public IReadOnlyList<KAfterRule> AfterRules { get; protected set; } = new List<KAfterRule>();

        public abstract void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append);
    }
}
