using System;
using System.Xml;

namespace TypiconOnline.Domain.Rules
{
    //public class RulesParsingException : XmlException
    //{
    //    public RulesParsingException()
    //    {
    //    }

    //    public RulesParsingException(string message)
    //        : base(message)
    //    {
    //    }

    //    public RulesParsingException(string message, Exception inner)
    //        : base(message, inner)
    //    {
    //    }
    //}

    public class RulesNotInterpretedException : XmlException
    {
        public RulesNotInterpretedException() : base("Ошибка: необходимо сначала вызвать метод Interpret()")
        {

        }

        public RulesNotInterpretedException(string message)
            : base(message)
        {
        }

        public RulesNotInterpretedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
