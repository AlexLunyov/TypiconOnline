﻿using JqueryDataTables.ServerSide.AspNetCoreWeb;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.WebQuery.Interfaces;

namespace TypiconOnline.Domain.WebQuery.Models
{
    public class TypiconEntityFilteredModel: TypiconEntityModel, IGridModel
    {
        [Sortable]
        public string Status { get; set; }
        public bool Editable { get; set; }
        public bool Deletable { get; set; }
        public bool Approvable { get; set; }
    }
}