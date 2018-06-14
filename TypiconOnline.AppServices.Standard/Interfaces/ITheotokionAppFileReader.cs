﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books.Elements;
using TypiconOnline.Domain.Rules;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface ITheotokionAppFileReader 
    {
        string Read(TheotokionAppPlace place, int ihos, DayOfWeek dayOfWeek);
    }
}
