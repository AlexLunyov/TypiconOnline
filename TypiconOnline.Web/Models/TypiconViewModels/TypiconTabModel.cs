using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypiconOnline.Web.Models.TypiconViewModels
{
    public class TypiconTabModel
    {
        public int TypiconId { get; set; }
        public TypiconTab Tab { get; set; }
        public string Name { get; set; }
        public int VariablesCount { get; set; }
        public int EmptyPrintTemplatesCount { get; set; }
        public bool UserIsAuthor { get; set; }
        public bool IsTemplate { get; set; }
        public bool ScheduleSettingsExist { get; set; }
    }

    public enum TypiconTab
    {
        Properties,
        Operations,
        Editors,
        PrintTemplate,
        Variables,
        ScheduleSettings,
        Sign,
        Menology,
        Triodion,
        Common,
        Explicit,
        Kathizma,
        /// <summary>
        /// Редактирование или создание любого вложенного Правила
        /// </summary>
        CreateSign,
        EditSign,
        CreateMenology,
        EditMenology,
        CreateTriodion,
        EditTriodion,
        CreateCommon,
        EditCommon,
        CreateExplicit,
        EditExplicit,
        EditKathizma,
        CreatePrintTemplate,
        EditPrintTemplate,
        EditVariable
    }
}
