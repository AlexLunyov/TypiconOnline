using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.Infrastructure.Common.ErrorHandling;

namespace TypiconOnline.AppServices.Jobs
{
    public class LoggingJobRepository : IJobRepository
    {
        private readonly IJobRepository _repo;
        private readonly ILogger _logger;

        public LoggingJobRepository(IJobRepository repo, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _repo = repo ?? throw new ArgumentNullException(nameof(repo));

            _logger = loggerFactory.CreateLogger("JobRepository");
        }
        public Result Create(IJob job)
        {
            var result = _repo.Create(job);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Created {job.GetType().Name}");
            });

            return result;
        }

        public Result Create(IJob job, DateTime date)
        {
            var result = _repo.Create(job, date);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Created {job.GetType().Name} for {date}");
            });

            return result;
        }

        public Result Fail(IJob job, string message)
        {
            var result = _repo.Fail(job, message);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Failed {job.GetType().Name} with message: {message}");
            });

            return result;
        }

        public Result Finish(IJob job)
        {
            var result = _repo.Finish(job);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Finished {job.GetType().Name} at {DateTime.Now}");
            });

            return result;
        }

        public Result Finish(IJob job, string message)
        {
            var result = _repo.Finish(job, message);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Finished {job.GetType().Name} at {DateTime.Now} with message: {message}");
            });

            return result;
        }

        public IEnumerable<IJob> GetAll()
        {
            var result = _repo.GetAll();

            _logger.Log(LogLevel.Information, $"Called GetAll() with {result.Count()} job result");

            return result;
        }

        public Result Recreate(IJob job)
        {
            var result = _repo.Recreate(job);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Recreated {job.GetType().Name} at {DateTime.Now}");
            });

            return result;
        }

        public Result Recreate(IJob job, int millisecondsDelay)
        {
            var result = _repo.Recreate(job, millisecondsDelay);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Recreated {job.GetType().Name} at {DateTime.Now} with {millisecondsDelay} milliseconds delay");
            });

            return result;
        }

        public IEnumerable<IJob> Reserve(int count)
        {
            var result = _repo.Reserve(count);

            if (result.Count() > 0)
            {
                _logger.Log(LogLevel.Information, $"Called Reserve({count}) with {result.Count()} job result");
            }

            return result;
        }

        public Result Start(IJob job)
        {
            var result = _repo.Start(job);

            result.OnSuccess(() =>
            {
                _logger.Log(LogLevel.Information, $"Started {job.GetType().Name} at {DateTime.Now}");
            });

            return result;
        }
    }
}
