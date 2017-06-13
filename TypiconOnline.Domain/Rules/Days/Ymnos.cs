using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Rules.Days
{
    public class Ymnos : ItemText
    {
        public Ymnos()
        {
            Stihos = new ItemText();
        }
        public ItemText Stihos { get; set; }
    }
}
