using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Services
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProcess serviceProcess;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceProcess serviceProcess, ILogger<Worker> logger)
        {
            this.serviceProcess = serviceProcess;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await serviceProcess.Crawldata();
            }
        }
    }
}
