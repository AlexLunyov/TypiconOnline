using System;
using System.Collections;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers.CustomParameters
{
    /// <summary>
    /// Коллекция особых параметров обработки Правил
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomParamsCollection<T> : ICollection<T> where T: IRuleCustomParameter
    {
        Dictionary<Type, T> Elements { get; } = new Dictionary<Type, T>();

        public CustomParamsCollection()
        {

        }

        public CustomParamsCollection(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        public int Count => Elements.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (!Contains(item))
            {
                Elements.Add(item.GetType(), item);
            }
        }

        public void Clear()
        {
            Elements.Clear();
        }

        public bool Contains(T item)
        {
            return Elements.ContainsValue(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Elements.Values.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Elements)
            {
                yield return item.Value;
            }
        }

        public bool Remove(T item)
        {
            return Elements.Remove(item.GetType());
        }

        public bool TrueForAll(Predicate<T> match)
        {
            bool result = true;
            foreach (var item in Elements)
            {
                result = result && match(item.Value);
            }
            return result;
        }

        public void ForEach(Action<T> action)
        {
            foreach (var item in Elements)
            {
               action(item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
