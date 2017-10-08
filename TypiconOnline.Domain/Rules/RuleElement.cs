using System;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Rules
{
    public abstract class RuleElement : ValueObjectBase
    {
        //RuleElement ParentElement { get;set; }

        protected string ElementName { get; set; }

        public RuleElement() { }

        public RuleElement(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("RuleElement");         
               
            ElementName = node.Name;
        }

        private bool _isInterpreted = false;

        /// <summary>
        /// Общедоступный метод вызова интерпретации правила
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        public virtual void Interpret(DateTime date, IRuleHandler handler)
        {
            //Проверка для всех элементов правил. 
            //Если неверно составлен, то либо выкидывается исключение (в случае соответствующей настройки),
            //либо просто ничего не обрабатывается
            if (!IsValid)
            {
                if (handler.Settings.ThrowExceptionIfInvalid)
                {
                    ThrowExceptionIfInvalid();
                }
                else
                {
                    return;
                }
            }

            InnerInterpret(date, handler);
            _isInterpreted = true;
        }


        public bool IsInterpreted
        {
            get
            {
                return _isInterpreted;
            }
        }

        /// <summary>
        /// Внутренний метод для определения интерпретации. Должен быть определен в каждом элементе
        /// </summary>
        /// <param name="date"></param>
        /// <param name="handler"></param>
        protected abstract void InnerInterpret(DateTime date, IRuleHandler handler);
    }
}

