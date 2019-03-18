using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Jobs;

namespace TypiconOnline.AppServices.Interfaces
{
    public interface IQueue
    {
        void Send<T>(T job) where T : IJob;

        IEnumerable<T> ExtractAll<T>() where T : IJob;
        IEnumerable<T> Extract<T>(int count) where T : IJob;
    }
}
