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

        public virtual List<RuleElement> ChildElements { get; set; } = new List<RuleElement>();

        #endregion

        #region Methods

        protected override void InnerInterpret(IRuleHandler handler)
        {
            //Добавляем IAsAdditionElement append реализацию
            AppendHandling(handler);

            foreach (RuleElement el in ChildElements)
            {
                el.Interpret(handler);
            }
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
            //чтобы создать список источников канонов на обработку
            var childrenHandler = new CollectorRuleHandler<T>() { Settings = settings };

            foreach (RuleElement elem in ChildElements)
            {
                elem.Interpret(childrenHandler);
            }

            var result = childrenHandler.GetResult();

            return (predicate != null) ? result.Where(predicate).ToList() : result;
        }

        /// <summary>
        /// Добавляет к текущему элементу все дочерние элементы из дополнения, помеченные как append
        /// </summary>
        /// <param name="handler"></param>
        protected void AppendHandling(IRuleHandler handler)
        {
            if (this is IAsAdditionElement rewritableElement && handler.Settings.Addition?.RuleContainer is ExecContainer container)
            {
                //ищем элементы, у которых Parent - с таким же именем как и ExecContainer,
                //а также AsAdditionMode == Append
                var found = container.GetChildElements<IAsAdditionElement>(handler.Settings.Addition,
                        c => c.AsAdditionMode == AsAdditionMode.Append && c.Parent?.AsAdditionName == rewritableElement.AsAdditionName);

                if (found.Count > 0)
                {
                    ChildElements.AddRange(found.Cast<RuleElement>());
                }
            }
        }

        #endregion
    }
}

