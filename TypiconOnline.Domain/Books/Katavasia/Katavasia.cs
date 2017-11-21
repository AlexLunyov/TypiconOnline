using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Books.Katavasia
{
    public class Katavasia : EntityBase<int>, IAggregateRoot, IBookElement<Kanonas>
    {
        private Kanonas _kanonas;

        public string Name { get; set; }

        public string Definition { get; set; }

        public Kanonas GetElement()
        {
            ThrowExceptionIfInvalid();

            if (_kanonas == null)
            {
                _kanonas = new TypiconSerializer().Deserialize<Kanonas>(Definition);
            }

            return _kanonas;
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Definition))
            {
                AddBrokenConstraint(KatavasiaBusinessConstraint.EmptyStringDefinition);
            }
        }
    }
}
