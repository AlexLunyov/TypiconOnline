using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleForm
{
    public class MineinikNotFoundException : Exception
    {
        public MineinikNotFoundException()
        {
        }

        public MineinikNotFoundException(string message)
            : base(message)
        {
        }

        public MineinikNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class TriodionNotFoundException : Exception
    {
        public TriodionNotFoundException()
        {
        }

        public TriodionNotFoundException(string message)
            : base(message)
        {
        }

        public TriodionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class EasterNotFoundException : Exception
    {
        public EasterNotFoundException()
        {
        }

        public EasterNotFoundException(string message)
            : base(message)
        {
        }

        public EasterNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ServiceSignNotFoundException : Exception
    {
        public ServiceSignNotFoundException()
        {
        }

        public ServiceSignNotFoundException(string message)
            : base(message)
        {
        }

        public ServiceSignNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class DayWithoutServicesException : Exception
    {
        public DayWithoutServicesException()
        {
        }

        public DayWithoutServicesException(string message)
            : base(message)
        {
        }

        public DayWithoutServicesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
