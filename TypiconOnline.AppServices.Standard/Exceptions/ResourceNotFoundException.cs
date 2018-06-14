﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypiconOnline.AppServices.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message)
            : base(message)
        { }

        public ResourceNotFoundException()
            : base("The requested resource was not found.")
        { }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
