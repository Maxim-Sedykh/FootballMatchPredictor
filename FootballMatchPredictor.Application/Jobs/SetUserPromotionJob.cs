using FootballMatchPredictor.Domain.Interfaces.Services;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Jobs
{
    public class SetUserPromotionJob : IJob
    {
        private readonly IUserProfileService _userProfileService;

        public SetUserPromotionJob(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _userProfileService.PromotionalBalanceIncrease();
        }
    }
}
