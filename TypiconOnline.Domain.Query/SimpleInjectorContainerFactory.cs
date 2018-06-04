using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Query
{
    public class SimpleInjectorContainerFactory
    {
        public static Container Create() => new Container().RegisterTypiconQueryClasses();
    }
}
