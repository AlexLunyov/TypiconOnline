using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Query;

namespace TypiconOnline.Domain.Rules.Serialization
{
    /// <summary>
    /// Обертка для базового класса. Нужен для собирания в общую коллекцию всех десериализуемых элементов
    /// </summary>
    public class CollectorSerializerRoot : RuleSerializerRoot
    {
        private List<IRuleElement> _collection = new List<IRuleElement>();
        public CollectorSerializerRoot([NotNull] IQueryProcessor queryProcessor, ITypiconSerializer typiconSerializer) : base(queryProcessor, typiconSerializer)
        {
        }

        public override IRuleSerializerContainer<T> Container<T>()
        {
            return new CollectorContainer<T>(base.Container<T>(), this);
        }

        public void AddElement(IRuleElement element)
        {
            _collection.Add(element);
        }

        public void ClearElements() => _collection.Clear();

        public IEnumerable<T> GetElements<T>()
        {
            return _collection
                .Where(c => c is T)
                .Cast<T>();
        }
    }
}
