using System.Collections.Generic;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Базовый абстрактный generic класс для структур песнопений (стихиры, эксапостиларии, тропари, седальны, блаженны и т.д.).
    /// </summary>
    /// <typeparam name="TStructure"></typeparam>
    /// <typeparam name="TChildElement">Тип дочерних элементов-Правил, из которых составляется наполнение Структуры</typeparam>
    public abstract class StructureRuleBase<TStructure, TChildElement> : ExecContainer, ICustomInterpreted
        where TStructure : DayElementBase, IMergable<TStructure>, new()
        where TChildElement : IStructureRuleChildElement<TStructure>
    {
        public StructureRuleBase(string name) : base(name) { }

        /// <summary>
        /// Вычисленная последовательность богослужебных текстов
        /// </summary>
        public TStructure Structure { get; private set; }

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (IsAuthorized(handler))
            {
                //используем специальный обработчик для YmnosStructureRule,
                //чтобы создать список источников стихир на обработку
                var container = GetChildElements<TChildElement>(handler.Settings);

                if (container != null)
                {
                    CalculateStructure(handler.Settings, container);
                }

                handler.Execute(this);
            }
        }

        private void CalculateStructure(RuleHandlerSettings settings, IReadOnlyList<TChildElement> container)
        {
            Structure = new TStructure();

            foreach (var element in container)
            {
                TStructure elemStructure = element.GetStructure(settings);

                if (elemStructure != null)
                {
                    Structure.Merge(elemStructure);
                }
            }
        }

        /// <summary>
        /// Метод должен определяться для каждого класса-наследника
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected abstract bool IsAuthorized(IRuleHandler handler);
    }
}
