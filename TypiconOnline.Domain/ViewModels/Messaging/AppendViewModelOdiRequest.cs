using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Days;

namespace TypiconOnline.Domain.ViewModels.Messaging
{
    public class AppendViewModelOdiRequest
    {
        public Odi Odi { get; set; }
        public bool IsLastKanonas { get; set; }
        public bool IsOdi8 { get; set; }
        public string DefaultChorus { get; set; }
    }
}
