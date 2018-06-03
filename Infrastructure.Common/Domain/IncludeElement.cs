using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Infrastructure.Common.Domain
{
    public class IncludeElement
    {
        public string Name { get; set; }

        public ICollection<IncludeElement> Children { get; set; } = new List<IncludeElement>();
    }
}
