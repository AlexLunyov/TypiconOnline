using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain
{
    public class ErrorConstants 
    {
        public static string Signs = "Signs";
        public static string MenologyRules = "MenologyRules";
        public static string Kathismas = "Kathismas";
        public static string Sign = nameof(Typicon.Sign);
        public static string CommonRule = nameof(Typicon.CommonRule);
        public static string MenologyRule = nameof(Typicon.MenologyRule);
        
        public static string TriodionRule = nameof(Typicon.TriodionRule);
        public static string ExplicitAddRule = nameof(Typicon.ExplicitAddRule);
        
        public static string Kathisma = nameof(Typicon.Psalter.Kathisma);
    }
}
