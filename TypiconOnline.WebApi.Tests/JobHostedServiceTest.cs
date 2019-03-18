using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Infrastructure.Common.Command;
using TypiconOnline.Tests.Common;
using TypiconOnline.WebApi.HostedService;
using Xunit;

namespace TypiconOnline.WebApi.Tests
{
    public class JobHostedServiceTest
    {
        [Fact]
        public void JobHostedService_Async()
        {
            int index = 0;

            var mock = new Mock<ICommandProcessor>();
            mock.Setup(c => c.ExecuteAsync(It.IsAny<ICommand>()))
                .Callback(() =>
                {
                    Task.Delay(100);
                    index++;
                });

            var service = new JobHostedService(GetMockQueue(), mock.Object);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task.Run(() => service.StartAsync(token));

            Thread.Sleep(250);
            cancelTokenSource.Cancel();

            Assert.Equal(3, index);
        }

        private IQueue GetMockQueue()
        {
            var mock = new Mock<IQueue>();
            mock.Setup(c => c.Extract<IJob>(It.IsAny<int>()))
                .Returns(new List<IJob>()
                {
                    new CalculateModifiedYearJob(1, 2019),
                    new CalculateModifiedYearJob(1, 2019),
                    new CalculateModifiedYearJob(1, 2019)
                });

            return mock.Object;
        }

        private void Factorial()
        {
            int result = 1;
            for (int i = 1; i <= 10; i++)
            {
                result *= i;
            }
        }
    }
}
