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
    /// Класс содержит описания текстов богослужения (возгласы священников, тексты для диаконов, чтецов и т.д.)
    /// </summary>
    public class TextHolder: RuleExecutable, ICustomInterpreted, IViewModelElement
    {
        public TextHolder(IRuleSerializerRoot serializer, string name) : base(name)
        {
            Serializer = serializer ?? throw new ArgumentNullException("IRuleSerializerRoot");
        }

        public TextHolder(TextHolder item)
        {
            if (item == null) throw new ArgumentNullException("TextHolder");

            Kind = item.Kind;
            Mark = item.Mark;

            foreach (ItemTextNoted text in item.Paragraphs)
            {
                Paragraphs.Add(new ItemTextNoted(text.StringExpression));
            }
        }

        //public TextHolder(Ymnos ymnos)
        //{
        //    if (ymnos == null) throw new ArgumentNullException("Ymnos");

        //    _textHolderKind = new ItemEnumType<TextHolderKind>() { Value = TextHolderKind.Choir };
            
        //    if (ymnos.Annotation != null)
        //    {

        //    }
        //}

        #region Properties

        public TextHolderKind Kind { get; set; }

        /// <summary>
        /// Пометка текста определенным знаком.
        /// </summary>
        public TextHolderMark Mark { get; set; } = TextHolderMark.undefined;

        public List<ItemTextNoted> Paragraphs { get; set; } = new List<ItemTextNoted>();

        public IRuleSerializerRoot Serializer { get; }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
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

        public ElementViewModel CreateViewModel(IRuleHandler handler)
        {
            return new TextHolderViewModel(this, handler);
        }
    }
}
