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
using TypiconOnline.Domain.Rules.Schedule.Extensions;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Правило для использования седальнов 
    /// </summary>
    public class KSedalenRule : KanonasItemRuleBase
    {
        public KSedalenRule(string name) : base(name) { }

        #region Properties

        /// <summary>
        /// Место в тексте богослужения для выбора канона
        /// </summary>
        public KanonasPlaceKind Place { get; set; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KSedalenRule>())
            {
                handler.Execute(this);
            }
        }

        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            YmnosStructure result = null;

            Kanonas kanonas = GetKanonas(settings);

            if (kanonas != null)
            {
                switch (Place)
                {
                    case KanonasPlaceKind.sedalen:
                        if (kanonas.Sedalen != null)
                        {
                            result = new YmnosStructure();
                            result.Groups.AddRange(kanonas.Sedalen.Groups);
                        }
                        break;
                    case KanonasPlaceKind.kontakion:
                        if (kanonas.Kontakion != null)
                        {
                            result = kanonas.Kontakion.ToYmnosStructure();
                        }
                        break;
                }
            }

            return result;
        }

        
    }
}
