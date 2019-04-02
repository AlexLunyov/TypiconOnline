﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Output;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IScheduleWeekViewer
    {
        void Execute(LocalizedOutputWeek week);
    }

    public interface IScheduleWeekViewer<T> where T : class
    {
        T Execute(LocalizedOutputWeek week);
    }
}
