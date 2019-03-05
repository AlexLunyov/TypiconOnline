using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Serialization
{
    /// <summary>
    /// Контейнер фабрик Xml-сериализации для элемента Правила или его наследников
    /// </summary>
    /// <typeparam name="T1">Елемент правила. Также учитываются все его наследники</typeparam>
    /// <typeparam name="T2">Второе условие</typeparam>
    /// <returns></returns>
    public class RuleXmlSerializerContainer<T1, T2> : RuleXmlSerializerContainer<T1> where T1 : IRuleElement
    {
        public RuleXmlSerializerContainer(IRuleSerializerRoot root) : base(root) { }

        protected override IEnumerable<Type> GetTypes()
        {
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                   from z in type.GetInterfaces()
                   where type.IsSubclassOf(typeof(RuleXmlSerializerBase))
                         
                         && z.Name == typeof(IRuleSerializer<T1>).Name
                         && (z.GenericTypeArguments[0].Equals(typeof(T1))
                             || z.GenericTypeArguments[0].IsSubclassOf(typeof(T1)))
                         && (z.GenericTypeArguments[0].Equals(typeof(T2))
                            || z.GenericTypeArguments[0].IsSubclassOf(typeof(T2))
                            || z.GenericTypeArguments[0].GetInterface(typeof(T2).ToString()) != null)
                   select type).Distinct();
        }
    }
}
