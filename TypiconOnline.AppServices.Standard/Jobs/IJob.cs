using TypiconOnline.Infrastructure.Common.Command;

namespace TypiconOnline.AppServices.Jobs
{
    public interface IJob : ICommand
    {
        JobStatus Status { get; set; }
    }

    public enum JobStatus
    {
        Created = 0,
        InProcess = 1,
        Finished = 2,
        Failed = 3
    }
}