using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;

namespace TypiconOnline.Domain.Typicon
{
    /// <summary>
    /// Класс определяет настройки при выполнении правил
    /// </summary>
    public class InterpretingSettings //: IRuleHandler
    {
        List<string> _authorizedElements = new List<string>();

        private bool _default = false;

        private TypiconRule _caller = null;

        private InterpretingSettings() { _default = true; }

        public InterpretingSettings(InterpretingMode mode)
        {
            switch (mode)
            {
                case InterpretingMode.ModificationDayOnly:
                    _authorizedElements.Add(RuleConstants.DayModificationNodeName);
                    break;
                case InterpretingMode.ScheduleOnly:
                    //TODO: Добавить элементы для обработки расписания богослужений
                    break;
                case InterpretingMode.ServiceConsistency:
                    //TODO: Добавить элементы для обработки последовательности службы
                    break;
            }
        }

        public InterpretingSettings(InterpretingMode mode, TypiconRule caller) : this(mode)
        {
            _caller = caller;
        }

        public TypiconRule Caller
        {
            get
            {
                return _caller;
            }
        }

        /// <summary>
        /// Метод определяет, входит ли элемент в настройки для выполнения правила
        /// </summary>
        /// <param name="elementName">Название элемента (из xml)</param>
        /// <returns></returns>
        public bool IsAuthorized(string elementName)
        {
            return (_default || _authorizedElements.Contains(elementName));
        }

        public bool IsAuthorized<T>() where T : ICustomInterpreted
        {
            return (typeof(T).Equals(typeof(DayModification)));
        }

        public void Execute(ICustomInterpreted element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает настройки, разрешающие выполнять все элементы подряд
        /// </summary>
        public static InterpretingSettings Default
        {
            get
            {
                return new InterpretingSettings();
            }
        }
    }

    /// <summary>
    /// Модификации настроек
    /// </summary>
    public enum InterpretingMode
    {
        /// <summary>
        /// Используется для построения списка ModifiedRules
        /// </summary>
        ModificationDayOnly,
        /// <summary>
        /// Используется для построения расписания
        /// </summary>
        ScheduleOnly,
        /// <summary>
        /// испольуется для построения последовательностей богослужения
        /// </summary>
        ServiceConsistency,
    }
}
