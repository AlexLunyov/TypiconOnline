﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Messaging.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IModifiedYearFactory
    {
        ModifiedYear Create(int typiconVersionId, int year);

        //ModifiedYear Create(TypiconVersion typiconVersion, int year);
    }
}
