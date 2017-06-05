using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Typicon
{
    public class TypiconRule : RuleEntity//, IRuleHandlerInitiator
    {
        
        public virtual Sign Template { get; set; }

        public virtual TypiconEntity Owner { get; set; }
        /// <summary>
        /// Возвращает Правило: либо свое, либо шаблонное
        /// </summary>
        public override RuleContainer Rule
        {
            get
            {
                if ((base.Rule == null) && string.IsNullOrEmpty(RuleDefinition))
                {
                    return Template.Rule;
                }

                return base.Rule;//(string.IsNullOrEmpty(RuleDefinition)) ? Template.Rule : null;
            }
        }

        protected string GetName(Day day, string selectedNames)
        {
            if (day == null)
                return "";

            string result = "";

            int[] indexes = null;
            if (!string.IsNullOrEmpty(selectedNames))
            {
                string[] separator = { ",", " " };
                string[] names = selectedNames.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    indexes = Array.ConvertAll(names, int.Parse);//names?.Select(int.Parse).ToArray();
                }
                catch { } 
            }

            if (indexes != null)
            {
                foreach (int index in indexes)
                {
                    ItemText itemText = day.DayName.Items.ElementAtOrDefault(index);
                    if (itemText != null)
                    {
                        result += GetTextByLanguage(itemText);
                    }
                }
            }
            else
            {
                foreach (ItemText itemText in day.DayName.Items)
                {
                    result += GetTextByLanguage(itemText);
                }
            }

            return result;
        }

        private string GetTextByLanguage(ItemText itemText)
        {
            return (itemText.Text.ContainsKey(Owner.Settings.DefaultLanguage)) 
                ? itemText.Text[Owner.Settings.DefaultLanguage] + " " : "";
        }
    }
}
