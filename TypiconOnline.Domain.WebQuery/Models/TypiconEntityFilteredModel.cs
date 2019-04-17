using JqueryDataTables.ServerSide.AspNetCoreWeb;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconEntityFilteredModel: TypiconEntityModel
    {
        [Sortable]
        public string Status { get; set; }
        public bool Editable { get; set; }
        public bool Deletable { get; set; }
        public bool Approvable { get; set; }
    }
}
