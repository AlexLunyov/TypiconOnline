using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Элемент определяет последовательность после N-ой песни канона
    /// </summary>
    public class KAfterRule : ExecContainer, ICustomInterpreted, IAsAdditionElement
    {
        public KAfterRule(string name, KanonasRule parent) : base(name)
        {
            Parent = parent ?? throw new ArgumentNullException("KanonasRule in KOdiRule");
        }
        /// <summary>
        /// Номер песни, после которой будут добавлены дочерние элементы Правила
        /// </summary>
        public int OdiNumber { get; set; }

        #region IRewritableElement implementation 
        /// <summary>
        /// Ссылка на KanonasRule
        /// </summary>
        public IAsAdditionElement Parent { get; }

        public string AsAdditionName
        {
            get
            {
                string result = $"{ElementName}?{RuleConstants.KAfterOdiNumberAttrName}={OdiNumber}";

                if (Parent != null)
                {
                    result = $"{Parent.AsAdditionName}/{result}";
                }

                return result;
            }
        }

        public AsAdditionMode AsAdditionMode { get; set; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KAfterRule>() && !this.AsAdditionHandled(handler))
            {
                base.InnerInterpret(handler);

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
