using System;
using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.AppServices.Jobs
{
    public interface IJob : ICommand, IEquatable<IJob>
    {
    }
}