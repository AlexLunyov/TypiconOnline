using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.WinServices.Messaging
{
    public class HandleTemplateRequest
    {
        public string FileTemplateName { get; set; }
        public string OutputFolderPath { get; set; }
        public string ScheduleFileStart { get; set; }
        public int DaysPerTable { get; set; } = 7;
        public bool OpenFileAfterHandling { get; set; }
        public OutputWeek ScheduleWeek { get; set; }
    }
}
