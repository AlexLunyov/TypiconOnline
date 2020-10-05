using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Rules.Variables;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент "Служба". Является строкой в расписании. 
    /// Включает в себя последовательность богослужения, задекларированного в названии
    /// </summary>
    public class WorshipRule : RuleExecutable, ICustomInterpreted, IAsAdditionElement, IHavingVariables
    {
        private ItemTextStyled _name = new ItemTextStyled();

        public WorshipRule(string name, IAsAdditionElement parent, IQueryProcessor queryProcessor) : base(name)
        {
            Parent = parent;

            QueryProcessor = queryProcessor;
        }

        #region Properties

        private IQueryProcessor QueryProcessor { get; }

        /// <summary>
        /// Идентификатор для поиска (используется в переопределении правил AsAddition)
        /// </summary>
        public string Id { get; set; }
        public VariableItemTime Time { get; set; }

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
        public ItemTextStyled AdditionalName { get; set; } = new ItemTextStyled();
        /// <summary>
        /// Последовательность богослужения
        /// </summary>
        public ExecContainer Sequence { get; set; }
        /// <summary>
        /// Указание, в какое место добавлять службу
        /// </summary>
        public WorshipMode Mode { get; set; } = WorshipMode.ThisDay;

        #endregion

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
                    result += $"?{RuleConstants.WorshipRuleIdAttrName}={Id}&mode={Mode}";
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

                Mode = s.Mode;

                if (s.Sequence != null)
                {
                    Sequence = s.Sequence;
                }
            }
        }

        #endregion

        #region IHavingVariables implementation

        public IEnumerable<(string, VariableType)> GetVariableNames()
        {
            var result = new List<(string, VariableType)>();
            
            if (!Time.HasValue)
            {
                result.Add((Time.VariableName, VariableType.Time));
            }

            return result;
        }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsTypeAuthorized(this) && !this.AsAdditionHandled(handler))
            {
                //попробуем взять значение из переменных Устава
                if (!Time.HasValue)
                {
                    Time = new VariableItemTime(Time.TryGetValue(handler.Settings.TypiconVersionId, QueryProcessor));
                }

                handler.Execute(this);

                Sequence?.Interpret(handler);
            }
        }

        protected override void Validate()
        {
            if (Time.HasValue && !Time.Value.IsValid)
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

