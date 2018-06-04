using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Rules.Handlers.IntConverters
{
    public class IntConverter : IIntConverter
    {
        public bool IsDigit(string str)
        {
            return int.TryParse(str, out int i);
        }

        public int Parse(string str)
        {
            int.TryParse(str, out int i);
            return i;
        }

        public string ToString(int value)
        {
            return value.ToString();
        }

        public bool TryParse(string str, out int value)
        {
            return int.TryParse(str, out value);
        }
    }
}
