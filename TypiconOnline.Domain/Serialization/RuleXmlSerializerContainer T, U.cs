using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.Domain.Serialization
{
    public class RuleXmlSerializerContainer<T, U> : RuleXmlSerializerContainer<T> where T : IRuleElement
    {
        public RuleXmlSerializerContainer(IRuleSerializerRoot root) : base(root) { }

        protected override IEnumerable<Type> GetTypes()
        {
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                   from z in type.GetInterfaces()
                   where type.IsSubclassOf(typeof(RuleXmlSerializerBase))
                         
                         && z.Name == typeof(IRuleSerializer<T>).Name
                         && (z.GenericTypeArguments[0].Equals(typeof(T))
                             || z.GenericTypeArguments[0].IsSubclassOf(typeof(T)))
                         && (z.GenericTypeArguments[0].Equals(typeof(U))
                            || z.GenericTypeArguments[0].IsSubclassOf(typeof(U))
                            || z.GenericTypeArguments[0].GetInterface(typeof(U).ToString()) != null)
                   select type).Distinct();
        }
    }
}
