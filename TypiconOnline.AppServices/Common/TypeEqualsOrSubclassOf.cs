using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.AppServices.Common
{
    public static class TypeEqualsOrSubclassOf<T> where T: class
    {
        public static bool Is(object obj)
        {
            if (obj == null) return false;

            return obj.GetType().Equals(typeof(T)) == true
                    //для Proxies
                    || obj.GetType().IsSubclassOf(typeof(T)) == true;
        }
    }
}
