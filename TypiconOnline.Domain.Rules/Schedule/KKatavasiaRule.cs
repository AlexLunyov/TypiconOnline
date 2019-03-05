using System;
using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Exceptions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Приавло для использования катавасии в каноне
    /// </summary>
    public class KKatavasiaRule : KanonasItemRuleBase
    {
        IDataQueryProcessor queryProcessor;

        public KKatavasiaRule(string name, [NotNull] IDataQueryProcessor queryProcessor, ITypiconSerializer serializer) 
            : base(name, serializer)
        {
            this.queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        }

        #region Properties

        private Kanonas _katavasia;

        public string Name { get; set; }

        #endregion

        protected override void InnerInterpret(IRuleHandler handler)
        {
            if (handler.IsAuthorized<KKatavasiaRule>())
            {
                handler.Execute(this);
            }
        }

        protected override void Validate()
        {
            //если name не определено, то тогда используем валидацию базового класса
            if (string.IsNullOrEmpty(Name))
            {
                base.Validate();
            }
            //находим в хранилище
            else
            {
                try
                {
                    GetFromRepository();
                }
                catch (ResourceNotFoundException)
                {
                    AddBrokenConstraint(KKatavasiaRuleBusinessConstraint.InvalidName, ElementName);
                }
            }
            
            //двойное определение
            if (!string.IsNullOrEmpty(Name)
                && (Source != null || Kanonas != null))
            {
                AddBrokenConstraint(KKatavasiaRuleBusinessConstraint.DoubleDefinition, ElementName);
            }
        }

        public override DayElementBase Calculate(RuleHandlerSettings settings)
        {
            return (string.IsNullOrEmpty(Name)) ? GetFromSource(settings) : GetFromRepository();
        }

        private Kanonas GetFromRepository()
        {
            if (_katavasia == null)
            {
                _katavasia = queryProcessor.Process(new KatavasiaQuery(Name));
            }

            return _katavasia;
        }

        private Kanonas GetFromSource(RuleHandlerSettings settings)
        {
            Kanonas result = null;
            Kanonas source = GetKanonas(settings);

            if (source != null)
            {
                result = new Kanonas()
                {
                    Acrostic = source.Acrostic,
                    Annotation = source.Annotation,
                    Ihos = source.Ihos,
                    Stihos = source.Stihos
                };

                foreach (Odi odi in source.Odes)
                {
                    Odi o = new Odi() { Number = odi.Number };

                    //добавляем катавасию(и)
                    o.Troparia.AddRange(odi.Troparia.FindAll(c => c.Kind == YmnosKind.Katavasia));
                    //добавляем саму песнь
                    result.Odes.Add(o);
                }
            }

            return result;
        }
    }
}
