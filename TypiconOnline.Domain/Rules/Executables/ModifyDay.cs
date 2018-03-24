using System;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Domain.Rules.Executables
{
    //
    //  EXAMPLES
    //
    //  <modifyday >
    //	    <getclosestday dayofweek = "saturday" weekcount="-2"><date>--11-08</date></getclosestday>
    //  </modifyday>
    //
    //  <modifyday daymove="0" priority="2" islastname="true"/>

    /// <summary>
    /// Элемент, используемый для переноса богослужебных дней
    /// </summary>
    public class ModifyDay : RuleExecutable, ICustomInterpreted
    {
        public ModifyDay(string name) : base(name) { }

        #region Properties
        /// <summary>
        /// Количество дней, на которые необходимо перенести день. Может иметь отрицательное значение
        /// </summary>
        public int? DayMoveCount { get; set; }
        /// <summary>
        /// Выставляемый приоритет изменяемому дню
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Краткое наименование праздника
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// Если true, в Расписании имя дня указывается последним
        /// </summary>
        public bool IsLastName { get; set; } 
        /// <summary>
        /// Используется как дополнение к имеющимся Правилам, не замещая их
        /// </summary>
        public bool AsAddition { get; set; } 
        /// <summary>
        /// Признак, использовать ли полное имя в Расписании
        /// </summary>
        public bool UseFullName { get; set; }
        /// <summary>
        /// Предустановленный номер Знака службы, который будет использоваться только для отображения в Расписании.
        /// Никак не влияет на обработку последовательности богослужений.
        /// </summary>
        public int? SignNumber { get; set; }
        /// <summary>
        /// Фильтр для текстов служб
        /// </summary>
        public DayWorshipsFilter Filter { get; set; } = new DayWorshipsFilter();
        /// <summary>
        /// Вычисляемое выражение для переноса даты
        /// </summary>
        public DateExpression ChildDateExp { get; set; }
        /// <summary>
        /// Правило для дня, который будет перемещен
        /// </summary>
        public ModifyReplacedDay ModifyReplacedDay { get; set; }

        public ItemDate MoveDateExpression
        {
            get
            {
                //if (!IsInterpreted)
                //    throw new DefinitionsNotInterpretedException();

                //if (!IsInterpreted || (_childDateExp == null))
                //    return DateTime.MinValue;

                return ChildDateExp?.ValueExpression as ItemDate;
            }
        }

        /// <summary>
        /// Возвращает true, если все свойства имеют значения по умолчанию
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (DayMoveCount == null
                    && Priority == 0
                    && string.IsNullOrEmpty(ShortName)
                    && !IsLastName
                    && !AsAddition
                    && UseFullName
                    && SignNumber == null
                    && ChildDateExp == null
                    && ModifyReplacedDay == null);
            }
        }

        public DateTime MoveDateCalculated { get; private set; }

        #endregion

        #region Methods

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (IsValid && handler.IsAuthorized<ModifyDay>())
            {
                InterpretChildDateExp(handler);

                handler.Execute(this);

                //обработка ModifyReplacedDay

                DateTime date = handler.Settings.Date;

                handler.Settings.Date = MoveDateCalculated;

                ModifyReplacedDay?.Interpret(handler);
                //возвращаем на всякий случай обратно дату
                handler.Settings.Date = date;
            }
        }

        /// <summary>
        /// Интерпретирует определение даты элемента
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        protected void InterpretChildDateExp(IRuleHandler handler)
        {
            if (ChildDateExp != null)
            {
                ChildDateExp.Interpret(handler);
                MoveDateCalculated = (DateTime)ChildDateExp.ValueCalculated;
            }
            else
            {
                MoveDateCalculated = handler.Settings.Date.AddDays((int) DayMoveCount);
            }
        }

        protected override void Validate()
        {
            //if (!_isLastName.IsValid)
            //{
            //    AddBrokenConstraint(ModifyDayBusinessConstraint.IsLastNameTypeMismatch, ElementName);
            //}

            //if (!_asAddition.IsValid)
            //{
            //    AddBrokenConstraint(ModifyDayBusinessConstraint.IsLastNameTypeMismatch, ElementName);
            //}

            //if (!_useFullName.IsValid)
            //{
            //    AddBrokenConstraint(ModifyDayBusinessConstraint.UseFullNameTypeMismatch, ElementName);
            //}

            if (IsEmpty)
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.EmptyElement, ElementName);
            }

            if ((DayMoveCount != null) && (ChildDateExp != null))
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.DateDoubleDefinition, ElementName);
            }
            else if ((DayMoveCount == null) && (ChildDateExp == null))
            {
                AddBrokenConstraint(ModifyDayBusinessConstraint.DateAbsense, ElementName);
            }

            //добавляем ломаные правила к родителю
            if (ChildDateExp?.IsValid == false)
            {
                AppendAllBrokenConstraints(ChildDateExp, ElementName);
            }

            if (ModifyReplacedDay?.IsValid == false)
            {
                AppendAllBrokenConstraints(ModifyReplacedDay, ElementName);
            }

            if (Filter?.IsValid == false)
            {
                AppendAllBrokenConstraints(Filter, ElementName);
            }
        }

        #endregion
    }
}

