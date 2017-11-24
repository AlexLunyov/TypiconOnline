﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Serialization;

namespace TypiconOnline.Domain.Interfaces
{
    public interface IRuleSerializerUnitOfWork
    {
        RuleBaseSerializerContainer<T> Factory<T>() where T : RuleElement;
    }
}