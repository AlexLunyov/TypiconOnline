using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.ViewModels
{
    /// <summary>
    /// Константы для выходных форм последовательностей богослужения
    /// </summary>
    public static class ViewModelConstants
    {
        /*
         * Константы для элементов класса ViewModelRoot
        */
        public const string ViewModelRootNodeName = "viewmodel";

        /*
         * Константы для элементов класса WorshipRuleViewModel
        */
        public const string WorshipRuleNodeName = "worship"; 
        public const string WorshipRuleIdAttrName = "id"; 
        public const string WorshipRuleTimeAttrName = "time";
        public const string WorshipRuleNameAttrName = "name"; 
        public const string WorshipRuleAdditionalNameAttrName = "additionalname"; 
        public const string WorshipRuleChildNodeName = "items";

        /*
         * Константы для элементов класса ViewModelItem
        */
        public const string ViewModelItemNodeName = "item";
        public const string ViewModelKindTextAttrName = "kindtext"; 
        public const string ViewModelKindAttrName = "kind"; 
        public const string ViewModelItemChildNodeName = "paragraphs";

        /*
         * Константы для элементов класса ParagraphViewModel 
        */
        public const string ParagraphNodeName = "p";
        public const string ParagraphLanguageNodeName = "language";
        public const string ParagraphTextNodeName = "text";
        public const string ParagraphStyleNodeName = "style";
        public const string ParagraphNoteNodeName = "note";
    }
}
