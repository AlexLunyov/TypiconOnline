using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Interfaces
{
    public interface ILocalizable<T> where T: class
    {
        T Localize(string language);
    }
}
