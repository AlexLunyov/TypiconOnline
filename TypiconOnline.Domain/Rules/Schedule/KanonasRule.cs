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
using TypiconOnline.Domain.ViewModels.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для составления канонов
    /// </summary>
    public class KanonasRule : IncludingRulesElement, ICustomInterpreted, IViewModelElement
    {
        public KanonasRule(string name, IRuleSerializerRoot serializerRoot, 
            IElementViewModelFactory<KanonasRule> viewModelFactory) : base(name, serializerRoot)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory in KanonasRule");
        }

        #region Properties

        private List<Kanonas> _kanonesCalc = new List<Kanonas>();

        /// <summary>
        /// Вычисленные каноны правила
        /// </summary>
        public IReadOnlyList<Kanonas> Kanones => _kanonesCalc.AsReadOnly();

        /// <summary>
        /// Коллекция дочерних элементов, описывающих правила после n-ой песни канона
        /// </summary>
        public IEnumerable<KAfterRule> AfterRules { get; private set; }

        protected IElementViewModelFactory<KanonasRule> ViewModelFactory { get; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasRule>())
            {
                ///используем специальный обработчик для KKatavasiaRule
                var katavasiaContainer = GetChildElements<KKatavasiaRule>(handler);

                //используем специальный обработчик для KanonasItem,
                //чтобы создать список источников канонов на обработку
                var kanonasItemContainer = GetChildElements<KKanonasItemRule>(handler);

                if (kanonasItemContainer != null)
                {
                    CalculateOdesStructure(handler, kanonasItemContainer, (katavasiaContainer != null));
                }

                if (katavasiaContainer != null)
                {
                    CalculateKatavasiaStructure(handler, katavasiaContainer);
                }

                //находим KAfterRules
                var afterContainer = GetChildElements<KAfterRule>(handler);

                if (afterContainer != null)
                {
                    AfterRules = afterContainer.ChildElements.Cast<KAfterRule>();
                }

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            base.Validate();
        }

        /// <summary>
        /// Добавляет в конец коллекции вычисляемых канонов катавасию
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        /// <param name="container"></param>
        private void CalculateKatavasiaStructure(IRuleHandler handler, ExecContainer container)
        {
            if (container?.ChildElements.FirstOrDefault() is KKatavasiaRule item)
            {
                _kanonesCalc.Add(item.Calculate(handler.Settings) as Kanonas);
            }
        }


        private void CalculateOdesStructure(IRuleHandler handler, ExecContainer container, bool katavasiaExists)
        {
            for (int i = 0; i < container.ChildElements.Count; i++)
            {
                KKanonasItemRule item = container.ChildElements[i] as KKanonasItemRule;

                if (item.Calculate(handler.Settings) is Kanonas k)
                {
                    _kanonesCalc.Add(k);
                }

                //определение катавасии отсутствует и канон последний
                if (!katavasiaExists && i == container.ChildElements.Count - 1)
                {
                    //добавляем еще один канон, который будет состоять ТОЛЬКО из катавасий после 3, 6, 8, 9-х песен
                    if (item.CalculateEveryDayKatavasia(handler.Settings) is Kanonas k1)
                    {
                        _kanonesCalc.Add(k1);
                    }
                }
            }
        }

        public virtual void CreateViewModel(IRuleHandler handler, Action<ElementViewModel> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<KanonasRule>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }
    }
}
