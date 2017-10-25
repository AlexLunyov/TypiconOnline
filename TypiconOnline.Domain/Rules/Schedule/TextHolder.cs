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
        private ItemEnumType<TextHolderKind> _textHolderKind;
        private ItemEnumType<TextHolderMark> _textHolderMark;
        private List<ItemTextNoted> _paragraphs = new List<ItemTextNoted>();

        public TextHolder(XmlNode node) : base(node)
        {
            _textHolderKind = new ItemEnumType<TextHolderKind>(node.Name);

            XmlAttribute attr = node.Attributes[RuleConstants.TextHolderMarkAttr];
            if (attr != null)
            {
                _textHolderMark = new ItemEnumType<TextHolderMark>(attr.Name);
            }

            if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    ItemTextNoted item = new ItemTextNoted((childNode.Name == RuleConstants.TextHolderPapragraphNode) ? childNode.OuterXml : string.Empty);

                    _paragraphs.Add(item);
                }
            }
        }

        public TextHolder(TextHolder item)
        {
            if (item == null) throw new ArgumentNullException("TextHolder");

            _textHolderKind = new ItemEnumType<TextHolderKind>(item.ElementName);

            if (item.Mark != null)
            {
                _textHolderMark = new ItemEnumType<TextHolderMark>() {  Value = item.Mark.Value};
            }

            foreach (ItemTextNoted text in item.Paragraphs)
            {
                Paragraphs.Add(new ItemTextNoted(text.StringExpression));
            }
        }

        public TextHolder(Ymnos ymnos)
        {
            if (ymnos == null) throw new ArgumentNullException("Ymnos");

            _textHolderKind = new ItemEnumType<TextHolderKind>() { Value = TextHolderKind.Choir };
            
            if (ymnos.Annotation != null)
            {

            }
        }

        #region Properties

        public ItemEnumType<TextHolderKind> Kind
        {
            get
            {
                return _textHolderKind;
            }
        }

        /// <summary>
        /// Пометка текста определенным знаком.
        /// </summary>
        public ItemEnumType<TextHolderMark> Mark
        {
            get
            {
                return _textHolderMark;
            }
        }

        public List<ItemTextNoted> Paragraphs
        {
            get
            {
                return _paragraphs;
            }
        }

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
            if (_paragraphs.Count == 0)
            {
                AddBrokenConstraint(TextHolderBusinessConstraint.ParagraphRequired, ElementName);
            }

            foreach (ItemTextNoted item in _paragraphs)
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
