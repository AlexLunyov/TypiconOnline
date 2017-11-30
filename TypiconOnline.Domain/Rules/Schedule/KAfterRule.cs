using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент определяет последовательность после N-ой песни канона
    /// </summary>
    public class KAfterRule : ExecContainer, ICustomInterpreted
    {
        public KAfterRule(string name) : base(name) { }
        /// <summary>
        /// Номер песни, после которой будут добавлены дочерние элементы Правила
        /// </summary>
        public int OdiNumber { get; set; }

        protected override void InnerInterpret(DateTime date, IRuleHandler handler)
        {
            if (handler.IsAuthorized<KAfterRule>())
            {
                base.InnerInterpret(date, handler);

                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            base.Validate();

            if (OdiNumber < 1 || OdiNumber > 9)
            {
                AddBrokenConstraint(KAfterRuleBusinessConstraint.OdiNumberInvalid, ElementName);
            }
        }
    }

    public class KAfterRuleBusinessConstraint
    {
        public static readonly BusinessConstraint OdiNumberInvalid = new BusinessConstraint("Номер песни канона должен быть в диапазоне от 1 до 9.");
    }
}
