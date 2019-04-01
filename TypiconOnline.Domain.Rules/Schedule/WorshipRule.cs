using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент "Служба". Является строкой в расписании. 
    /// Включает в себя последовательность богослужения, задекларированного в названии
    /// </summary>
    public class WorshipRule : RuleExecutable, ICustomInterpreted, IAsAdditionElement
    {
        private ItemTextStyled _name = new ItemTextStyled();

        public WorshipRule(string name, IAsAdditionElement parent) : base(name)
        {
            Parent = parent;
        }

        #region Properties
        /// <summary>
        /// Идентификатор для поиска (используется в переопределении правил AsAddition)
        /// </summary>
        public string Id { get; set; }
        public ItemTime Time { get; set; }

        public ItemTextStyled Name
        {
            get => _name;
            set
            {
                if (value != null)
                {
                    _name = value;
                }
            }
        }
        public bool IsDayBefore { get; set; } = false;
        public ItemTextStyled AdditionalName { get; set; } = new ItemTextStyled();
        /// <summary>
        /// Последовательность богослужения
        /// </summary>
        public ExecContainer Sequence { get; set; }
        /// <summary>
        /// Значение заполняется при обработке Правила с помощью CustomParmeter
        /// </summary>
        public HandlingMode ModeFromHandler { get; set; } = HandlingMode.All;

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

                if (!string.IsNullOrEmpty(Id))
                {
                    result += $"?{RuleConstants.WorshipRuleIdAttrName}={Id}";
                }

                return result;
            }
        }

        public AsAdditionMode AsAdditionMode { get; set; }

        public void RewriteValues(IAsAdditionElement source)
        {
            if (source is WorshipRule s)
            {
                if (s.Time != null)
                {
                    Time = s.Time;
                }

                if (s.Name != null)
                {
                    Name = s.Name;
                }

                if (s.AdditionalName != null)
                {
                    AdditionalName = s.AdditionalName;
                }

                IsDayBefore = s.IsDayBefore;

                if (s.Sequence != null)
                {
                    Sequence = s.Sequence;
                }
            }
        }

        #endregion

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<WorshipRule>() && !this.AsAdditionHandled(handler))
            {
                handler.Execute(this);

                Sequence?.Interpret(handler);
            }
        }

        protected override void Validate()
        {
            if (!Time.IsValid)
            {
                AddBrokenConstraint(WorshipRuleBusinessConstraint.TimeTypeMismatch, ElementName);
            }

            //if (string.IsNullOrEmpty(Name))
            if (Name == null)
            {
                AddBrokenConstraint(WorshipRuleBusinessConstraint.NameReqiured, ElementName);
            }

            if (Sequence?.IsValid == false)
            {
                AppendAllBrokenConstraints(Sequence, ElementName);
            }
        }
    }

    public class WorshipRuleBusinessConstraint
    {
        public static readonly BusinessConstraint TimeTypeMismatch = new BusinessConstraint("Неверный формат времени.");
        public static readonly BusinessConstraint NameReqiured = new BusinessConstraint("Отсутствует определение имени.");
        public static readonly BusinessConstraint IsDayBeforeTypeMismatch = new BusinessConstraint("Неверный формат логического обозначения.");
    }
}

