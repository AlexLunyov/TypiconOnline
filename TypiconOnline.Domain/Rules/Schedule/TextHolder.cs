using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Класс содержит описания текстов богослужения (возгласы священников, тексты диаконов, чтецов и т.д.)
    /// </summary>
    public class TextHolder: RuleExecutable, ICustomInterpreted
    {
        private ItemEnumType<TextHolderKind> _textHolderKind;
        private List<ItemText> _paragraphs = new List<ItemText>();

        public TextHolder(XmlNode node) : base(node)
        {
            _textHolderKind = new ItemEnumType<TextHolderKind>(node.Name);

            if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    ItemText item = new ItemText((childNode.Name == RuleConstants.TextHolderPapragraphNode) ? childNode.OuterXml : string.Empty);

                    _paragraphs.Add(item);
                }
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

        public List<ItemText> Paragraphs
        {
            get
            {
                return _paragraphs;
            }
        }

        #endregion

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            if (_paragraphs.Count == 0)
            {
                AddBrokenConstraint(TextHolderBusinessConstraint.ParagraphRequired, ElementName);
            }

            foreach (ItemText item in _paragraphs)
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
    }
}
