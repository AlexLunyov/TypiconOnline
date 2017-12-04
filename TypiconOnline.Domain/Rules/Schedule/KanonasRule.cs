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
        //public CommonRuleElement Panagias { get; set; }

        private List<Kanonas> _kanonesCalc = new List<Kanonas>();

        /// <summary>
        /// Вычисленные каноны правила
        /// </summary>
        public IReadOnlyList<Kanonas> Kanones => _kanonesCalc.AsReadOnly();

        /// <summary>
        /// Коллекция дочерних элементов, описывающих правила после n-ой песни канона
        /// </summary>
        public IEnumerable<KAfterRule> AfterRules { get; private set; }

        /// <summary>
        /// Седален по 3-й песне
        /// </summary>
        //public YmnosStructure Sedalen { get; private set; }
        /// <summary>
        /// Кондак по 6-ой песне
        /// </summary>
        //public Kontakion Kontakion { get; private set; }
        /// <summary>
        /// Эксапостиларий по 9-ой песне
        /// </summary>
        //public Exapostilarion Exapostilarion { get; private set; }

        protected IElementViewModelFactory<KanonasRule> ViewModelFactory { get; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KanonasRule>())
            {
                ///используем специальный обработчик для KKatavasiaRule
                var katavasiaContainer = GetChildElements<KKatavasiaRule>(date, handler);

                //используем специальный обработчик для KanonasItem,
                //чтобы создать список источников канонов на обработку
                var kanonasItemContainer = GetChildElements<KKanonasItemRule>(date, handler);

                if (kanonasItemContainer != null)
                {
                    CalculateOdesStructure(date, handler, kanonasItemContainer, (katavasiaContainer != null));
                }

                if (katavasiaContainer != null)
                {
                    CalculateKatavasiaStructure(date, handler, katavasiaContainer);
                }

                //используем специальный обработчик для KSedalenRule
                //var sedalenContainer = GetChildElements<KSedalenRule>(date, handler);

                //if (sedalenContainer != null)
                //{
                //    CalculateSedalenStructure(date, handler, sedalenContainer);
                //}

                ////используем специальный обработчик для KKontakionRule
                //var kontakionContainer = GetChildElements<KKontakionRule>(date, handler);

                //if (kontakionContainer != null)
                //{
                //    CalculateKontakionStructure(date, handler, kontakionContainer);
                //}

                //находим KAfterRules
                var afterContainer = GetChildElements<KAfterRule>(date, handler);

                if (afterContainer != null)
                {
                    AfterRules = afterContainer.ChildElements.Cast<KAfterRule>();

                    //_afterRules.ForEach(c => c.Interpret(date, handler));
                }
                

                //CommonRules
                //Panagias?.Interpret(date, handler);

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
        private void CalculateKatavasiaStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        {
            if (container?.ChildElements.FirstOrDefault() is KKatavasiaRule item)
            {
                _kanonesCalc.Add(item.Calculate(date, handler.Settings) as Kanonas);
            }
        }

        //private void CalculateSedalenStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        //{
        //    YmnosStructure sedalen = new YmnosStructure();

        //    foreach (KSedalenRule item in container.ChildElements)
        //    {
        //        if (item.Calculate(date, handler.Settings) is YmnosStructure s)
        //        {
        //            if (item is KSedalenTheotokionRule)
        //            {
        //                sedalen.Theotokion = s.Theotokion;
        //            }
        //            else
        //            {
        //                sedalen.Groups.AddRange(s.Groups);
        //            }
        //        }
        //    }

        //    Sedalen = sedalen;
        //}

        //private void CalculateKontakionStructure(DateTime date, IRuleHandler handler, ExecContainer container)
        //{
        //    if (container?.ChildElements.FirstOrDefault() is KKontakionRule item)
        //    {
        //        Kontakion = item.Calculate(date, handler.Settings) as Kontakion;
        //    }
        //}

        private void CalculateOdesStructure(DateTime date, IRuleHandler handler, ExecContainer container, bool katavasiaExists)
        {
            for (int i = 0; i < container.ChildElements.Count; i++)
            {
                KKanonasItemRule item = container.ChildElements[i] as KKanonasItemRule;

                if (item.Calculate(date, handler.Settings) is Kanonas k)
                {
                    _kanonesCalc.Add(k);
                }

                //определение катавасии отсутствует и канон последний
                if (!katavasiaExists && i == container.ChildElements.Count - 1)
                {
                    //добавляем еще один канон, который будет состоять ТОЛЬКО из катавасий после 3, 6, 8, 9-х песен
                    if (item.CalculateEveryDayKatavasia(date, handler.Settings) is Kanonas k1)
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
