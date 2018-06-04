using System;
using System.Collections.Generic;
using System.Text;

namespace TypiconOnline.Domain.Query.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceName)
            : base($"Запрашиваемый ресурс не был найден: {resourceName}.")
        { }

        public ResourceNotFoundException()
            : base("Запрашиваемый ресурс не был найден.")
        { }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
