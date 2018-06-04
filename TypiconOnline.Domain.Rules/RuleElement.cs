using System;
using System.Linq;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules
{
    public abstract class RuleElement : ValueObjectBase, IRuleElement
    {
        //RuleElement ParentElement { get;set; }

        public string ElementName { get; }

        public RuleElement() { }

        public RuleElement(string name)
        {
            ElementName = name;
        }

        /// <summary>
        /// Общедоступный метод вызова интерпретации правила
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        public virtual void Interpret(IRuleHandler handler)
        {
            if (handler == null) throw new ArgumentNullException("IRuleHandler in Interpret");
            if (handler.Settings == null) throw new ArgumentNullException("handler.Settings in Interpret");

            //Проверка для всех элементов правил. 
            //Если неверно составлен, то либо выкидывается исключение (в случае соответствующей настройки),
            //либо просто ничего не обрабатывается
            if (ThrowExceptionIfInvalid(handler.Settings))
            {
                return;
            }

            handler.Settings.ApplyCustomParameters(this);

            if (handler.Settings.CheckCustomParameters(this))
            {
                InnerInterpret(handler);
            }
        }

        /// <summary>
        /// Внутренний метод для определения интерпретации. Должен быть определен в каждом элементе
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        protected abstract void InnerInterpret(IRuleHandler handler);

        /// <summary>
        /// Выкидывает исключение, если настройки обработчика позволяют это
        /// </summary>
        /// <param name="handler"></param>
        /// <returns>Возвращает true, если элемент неверно заполнен</returns>
        public bool ThrowExceptionIfInvalid(RuleHandlerSettings settings)
        {
            if (!IsValid)
            {
                //if (settings.ThrowExceptionIfInvalid)
                //{
                //    ThrowExceptionIfInvalid();
                //}
                return true;
            }

            return false;
        }

        
    }
}

