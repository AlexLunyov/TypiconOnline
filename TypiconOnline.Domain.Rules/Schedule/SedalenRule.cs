﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.ViewModels;

namespace TypiconOnline.Domain.Rules.Schedule
{
    /// <summary>
    /// Класс для описания правил седльна по кафизме
    /// </summary>
    public class SedalenRule : YmnosStructureRule
    {
        public SedalenRule(IElementViewModelFactory<YmnosStructureRule> viewModelFactory, string name) 
            : base(viewModelFactory, name) { }
    }
}
