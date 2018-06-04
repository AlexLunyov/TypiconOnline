using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Infrastructure.Common.Domain;

namespace TypiconOnline.Domain.Core.ItemTypes
{
    class ItemTextBusinessConstraint
    {
        public static readonly BusinessConstraint LanguageMismatch = new BusinessConstraint("Неверный формат языка.");
    }
}
