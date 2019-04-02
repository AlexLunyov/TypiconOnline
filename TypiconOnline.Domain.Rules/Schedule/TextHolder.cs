using System;
using System.Collections.Generic;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Output;
using TypiconOnline.Domain.Rules.Output.Messaging;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Класс содержит описания текстов богослужения (возгласы священников, тексты для диаконов, чтецов и т.д.)
    /// </summary>
    public class TextHolder: RuleExecutable, ICustomInterpreted, IViewModelElement
    {
        public TextHolder(IElementViewModelFactory<TextHolder> viewModelFactory, string name) : base(name)
        {
            ViewModelFactory = viewModelFactory ?? throw new ArgumentNullException("IElementViewModelFactory<TextHolder> in TextHolder");
        }

        public TextHolder(TextHolder item)
        {
            if (item == null) throw new ArgumentNullException("TextHolder");

            Kind = item.Kind;
            Mark = item.Mark;

            foreach (ItemTextNoted text in item.Paragraphs)
            {
                Paragraphs.Add(new ItemTextNoted(text));
            }
        }

        #region Properties

        public TextHolderKind Kind { get; set; }

        /// <summary>
        /// Пометка текста определенным знаком.
        /// </summary>
        public TextHolderMark Mark { get; set; } = TextHolderMark.undefined;

        public List<ItemTextNoted> Paragraphs { get; set; } = new List<ItemTextNoted>();

        private IElementViewModelFactory<TextHolder> ViewModelFactory { get; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<TextHolder>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (Paragraphs.Count == 0)
            {
                AddBrokenConstraint(TextHolderBusinessConstraint.ParagraphRequired, ElementName);
            }

            foreach (ItemTextNoted item in Paragraphs)
            {
                if (item.IsEmpty)
                {
                    AddBrokenConstraint(TextHolderBusinessConstraint.ParagraphEmpty, ElementName); 
                }
                if (!item.IsValid)
                {
                    AppendAllBrokenConstraints(item);
                }
            }
        }

        public void CreateViewModel(IRuleHandler handler, Action<OutputElementCollection> append)
        {
            ViewModelFactory.Create(new CreateViewModelRequest<TextHolder>()
            {
                Element = this,
                Handler = handler,
                AppendModelAction = append
            });
        }
    }
}
