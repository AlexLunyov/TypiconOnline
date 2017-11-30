using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Rules.Handlers
{
    /// <summary>
    /// Настройки для обработки правил
    /// </summary>
    public class RuleHandlerSettings
    {
        /// <summary>
        /// Дополнение для текущего правила
        /// </summary>
        public RuleHandlerSettings Addition { get; set; }
        /// <summary>
        /// Правило для обработки
        /// </summary>
        public TypiconRule Rule { get; set; }
        public List<DayWorship> DayWorships { get; set; }
        public OktoikhDay OktoikhDay { get; set; }
        public HandlingMode Mode { get; set; }
        //public string ShortName { get; set; }
        //public bool UseFullName { get; set; }
        /// <summary>
        /// Язык для обработки
        /// </summary>
        public string Language { get; set; }

        public IEnumerable<IScheduleCustomParameter> CustomParameters { get; set; }

        public RuleHandlerSettings()
        {
            DayWorships = new List<DayWorship>();
            //PutSeniorRuleNameToEnd = false;
            CustomParameters = new List<IScheduleCustomParameter>();
            //UseFullName = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seniorTypiconRule">Главное правило для обработки</param>
        public RuleHandlerSettings(DayRule seniorTypiconRule) : this()
        {
            Rule = seniorTypiconRule;
        }

        //private bool? _throwExceptionIfInvalid = null;

        /// <summary>
        /// Признак, генерировать ли исключение в случае неверного составления правила при его обработке
        /// </summary>
        //public bool ThrowExceptionIfInvalid
        //{
        //    get
        //    {
        //        if (_throwExceptionIfInvalid == null)
        //        {
        //            _throwExceptionIfInvalid = Rule?.Owner.Settings.IsExceptionThrownWhenInvalid;
        //            if (_throwExceptionIfInvalid == null)
        //            {
        //                _throwExceptionIfInvalid = true;
        //            }
        //        }
        //        return (bool) _throwExceptionIfInvalid;
        //    }
        //    set
        //    {
        //        _throwExceptionIfInvalid = value;
        //    }
        //}

        /// <summary>
        /// Применяет кастомные праметры к элементу, если таковые найдутся - соответствующие его типу
        /// </summary>
        public void ApplyCustomParameters(RuleElement element)
        {
            if (CustomParameters == null) return;

            foreach (var param in CustomParameters)
            {
                param.Apply(element);
            }
        }
    }
}
