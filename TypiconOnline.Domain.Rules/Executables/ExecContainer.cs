using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Executables
{
    public class ExecContainer : RuleExecutable
    {
        public ExecContainer() { }

        public ExecContainer(string name) : base(name) { }

        #region Properties

        //public RuleElement ParentElement { get; set; }

        public virtual List<RuleElementBase> ChildElements { get; } = new List<RuleElementBase>();

        #endregion

        #region Methods

        protected override void InnerInterpret(IRuleHandler handler)
        {
            //Добавляем IAsAdditionElement append реализацию
            var appended = AppendHandling(handler);

            foreach (RuleElementBase el in ChildElements)
            {
                el.Interpret(handler);
            }

            //и сразу же их удаляем
            RemoveAppended(appended);
        }

        protected override void Validate()
        {
            if (ChildElements.Count == 0)
            {
                //HACK: убрана проверка на обязательное наличие дочерних элементов.
                //Необходимость в том, что существуют элементы IAsAdditionElement для удаления (Mode==Remove), 
                //которые не обязательны для заполнения
                //AddBrokenConstraint(ExecContainerBusinessConstraint.ExecContainerChildrenRequired);
            }
            else
            {
                foreach (var element in ChildElements)
                {
                    if (element == null)
                    {
                        AddBrokenConstraint(ExecContainerBusinessConstraint.InvalidChild); 
                    }
                    //добавляем ломаные правила к родителю
                    else if (!element.IsValid)
                    {
                        AppendAllBrokenConstraints(element);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает все дочерние элементы согласно введенного generic типа
        /// </summary>
        /// <typeparam name="T">Тип элемента правил для поиска</typeparam>
        /// <param name="predicate">Дополнительные условия для поиска</param>
        /// <returns></returns>
        public IReadOnlyList<T> GetChildElements<T>(RuleHandlerSettings settings, Func<T, bool> predicate = null) //where T : RuleExecutable, ICustomInterpreted
        {
            //используем специальный обработчик
            //чтобы найти все дочерние элементы по искомым признакам
            var childrenHandler = new CollectorRuleHandler<T>() { Settings = settings };

            //Interpret(childrenHandler);

            foreach (RuleElementBase elem in ChildElements)
            {
                elem.Interpret(childrenHandler);
            }

            var result = childrenHandler.GetResult();

            return (predicate != null) ? result.Where(predicate).ToList() : result;
        }

        public IReadOnlyList<IAsAdditionElement> GetAsAdditionChildElements(RuleHandlerSettings settings, IAsAdditionElement element, Func<IAsAdditionElement, bool> predicate = null)
        {
            //используем специальный обработчик
            //чтобы найти все дочерние элементы по искомым признакам
            var childrenHandler = new AsAdditionElementHandler(element, predicate) { Settings = settings };

            //Interpret(childrenHandler);

            foreach (RuleElementBase elem in ChildElements)
            {
                elem.Interpret(childrenHandler);
            }

            return childrenHandler.GetResult();
        }

        /// <summary>
        /// Добавляет к текущему элементу все дочерние элементы из дополнения, помеченные как append
        /// </summary>
        /// <param name="handler"></param>
        protected IEnumerable<RuleElementBase> AppendHandling(IRuleHandler handler)
        {
            var result = new List<RuleElementBase>();
            if (this is IAsAdditionElement rewritableElement && handler.Settings.Addition?.RuleContainer is ExecContainer container)
            {
                //ищем элементы, у которых Parent - с таким же именем как и ExecContainer,
                //а также AsAdditionMode == Append
                var found = container.GetChildElements<IAsAdditionElement>(handler.Settings.Addition,
                        c => c.AsAdditionMode == AsAdditionMode.Append && c.Parent?.AsAdditionName == rewritableElement.AsAdditionName);

                if (found.Count > 0)
                {
                    result.AddRange(found.Cast<RuleElementBase>());
                    ChildElements.AddRange(result);
                }
            }

            return result;
        }

        /// <summary>
        /// Удаляем из коллекции дочерних элементов ранее добавленные IAsAdditionElements
        /// </summary>
        protected void RemoveAppended(IEnumerable<RuleElementBase> collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var element in collection)
            {
                ChildElements.Remove(element);
            }
            //if (this is IAsAdditionElement additionElement)
            //{
            //    ChildElements.RemoveAll(c => (c is IAsAdditionElement a) 
            //                                && a.AsAdditionMode == AsAdditionMode.Append 
            //                                && a.Parent?.AsAdditionName == additionElement.AsAdditionName);
            //}
            
        }

        #endregion
    }
}

