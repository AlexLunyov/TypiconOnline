using System;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Executables
{
    /// <summary>
    /// Корневой элемент Правил
    /// </summary>
    public class RootContainer: ExecContainer, IAsAdditionElement
    {
        public RootContainer(string name) : base(name) { }

        #region IRewritableElement implementation

        public IAsAdditionElement Parent { get; }

        public string AsAdditionName
        {
            get
            {
                string result = ElementName;

                if (Parent != null)
                {
                    result = $"{Parent.AsAdditionName}/{result}";
                }

                return result;
            }
        }

        /// <summary>
        /// Поле никак не используется
        /// </summary>
        public AsAdditionMode AsAdditionMode { get; set; }

        /// <summary>
        /// Ничего не делаем.
        /// </summary>
        /// <param name="source"></param>
        public void RewriteValues(IAsAdditionElement source) { }

        #endregion

        /// <summary>
        /// Справочник для хранения коллекций IAsAdditionElement
        /// </summary>
        private Dictionary<string, List<IAsAdditionElement>> _addElementsDict = new Dictionary<string, List<IAsAdditionElement>>();

        /// <summary>
        /// Возвращает коллекцию элементов для замены, отвечающих условиям
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="element"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IReadOnlyList<IAsAdditionElement> GetAsAdditionChildElements(IRuleHandler handler, RuleHandlerSettings settings, IAsAdditionElement element, Func<IAsAdditionElement, bool> predicate = null)
        {
            #region old version
            ////используем специальный обработчик
            ////чтобы найти все дочерние элементы по искомым признакам
            //var childrenHandler = new AsAdditionElementHandler(element, predicate) { Settings = settings };

            ////Interpret(childrenHandler);

            //foreach (RuleElementBase elem in ChildElements)
            //{
            //    elem.Interpret(childrenHandler);
            //}

            //return childrenHandler.GetResult();
            #endregion

            var elements = _addElementsDict.GetValueOrDefault(GetKey(handler, settings));

            //Проверяем, существует ли коллекция
            if (elements == null) 
            {
                //создаем ее
                var childrenHandler = new CollectorRuleHandler<IAsAdditionElement>();

                Interpret(childrenHandler);

                elements = new List<IAsAdditionElement>(childrenHandler.GetResult());

                _addElementsDict.Add(GetKey(handler, settings), elements);
            }

            return elements.Where(c => c.AsAdditionName == element.AsAdditionName)
                    .Where(predicate)
                    .ToList();
        }

        /// <summary>
        /// Возвращает ключ для сохранения в справочнике
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private string GetKey(IRuleHandler handler, RuleHandlerSettings settings)
        {
            return handler.GetType().FullName + settings.Date.ToString();//.GetHashCode();
        }
    }
}
