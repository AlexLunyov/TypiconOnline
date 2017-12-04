using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Rules.Executables
{
    /// <summary>
    /// Класс реализует обработку xml-конструкции if-then-else
    /// </summary>
    public class If : RuleExecutable
    {
        public If(string name) : base(name) { }

        #region Properties

        public BooleanExpression Expression { get; set; }

        public ExecContainer ThenElement { get; set; }

        public ExecContainer ElseElement { get; set; }

        #endregion

        #region Methods

        protected override void InnerInterpret(IRuleHandler handler)
        {
            Expression.Interpret(handler);

            if ((bool)Expression.ValueCalculated)
            {
                ThenElement.Interpret(handler);
            }
            else
            {
                ElseElement?.Interpret(handler);
            }
        }

        protected override void Validate()
        {
            if (Expression == null)
            {
                AddBrokenConstraint(IfBusinessBusinessConstraint.ConditionRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!Expression.IsValid)
                {
                    AppendAllBrokenConstraints(Expression);
                }
            }

            if (ThenElement == null)
            {
                AddBrokenConstraint(IfBusinessBusinessConstraint.ThenRequired, ElementName);
            }
            else
            {
                //добавляем ломаные правила к родителю
                if (!ThenElement.IsValid)
                {
                    AppendAllBrokenConstraints(ThenElement);
                }
            }

            if (ElseElement?.IsValid == false)
            {
                AppendAllBrokenConstraints(ElseElement);
            }
        }

        #endregion
    }
}
