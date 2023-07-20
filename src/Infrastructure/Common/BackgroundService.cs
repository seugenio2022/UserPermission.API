using Microsoft.Extensions.Hosting;

namespace UserPermission.API.Infrastructure.Common
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        private Task executingTask;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            executingTask = ExecuteAsync(cancellationTokenSource.Token);

            if (executingTask.IsCompleted)
            {
                return executingTask;
            }

            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (executingTask == null)
            {
                return;
            }

            try
            {
                cancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }

        }

        public virtual void Dispose()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
