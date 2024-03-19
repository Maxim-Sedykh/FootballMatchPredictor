using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Jobs
{
    public class BackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(SetUserPromotionJob));
            options.AddJob<SetUserPromotionJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithCronSchedule("0 0 1 * * ?"));
        }
    }
}
