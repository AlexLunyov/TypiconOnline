using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент "Служба". Является строкой в расписании. 
    /// Включает в себя последовательность богослужения, задекларированного в названии
    /// </summary>
    public class WorshipRule : ExecContainer, ICustomInterpreted, IAsAdditionElement
    {
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
        public string Name { get; set; }
        public bool IsDayBefore { get; set; } = false;
        public string AdditionalName { get; set; }
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

        #endregion

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<WorshipRule>() && !this.AsAdditionHandled(handler))
            {
                base.InnerInterpret(handler);

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            if (!Time.IsValid)
            {
                AddBrokenConstraint(WorshipRuleBusinessConstraint.TimeTypeMismatch, ElementName);
            }

            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenConstraint(WorshipRuleBusinessConstraint.NameReqiured, ElementName);
            }

            foreach (RuleElement element in ChildElements)
            {
                //добавляем ломаные правила к родителю
                if (!element.IsValid)
                {
                    foreach (BusinessConstraint brokenRule in element.GetBrokenConstraints())
                    {
                        AddBrokenConstraint(brokenRule);//, ElementName + "." + brokenRule.ConstraintPath);
                    }
                }
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

