﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.Domain.Core.Interfaces
{
    public interface IMergable<T> where T : class
    {
        void Merge(T source);
    }
}
