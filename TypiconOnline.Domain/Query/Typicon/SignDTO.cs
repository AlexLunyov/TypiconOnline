using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class SignDTO
    {
        public int Id { get; set; }
        public SignDTO Template { get; set; }
        public int Number { get; set; }
        public int Priority { get; set; }
        public bool IsTemplate { get; set; }
        public bool IsAddition { get; set; }
        /// <summary>
        /// Наименование знака службы на нескольких языках
        /// </summary>
        public ItemText SignName { get; set; }

        public string RuleDefinition { get; set; }
    }
}
