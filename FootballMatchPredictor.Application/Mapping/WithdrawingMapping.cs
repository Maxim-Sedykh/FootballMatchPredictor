using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.ViewModels.Match;
using FootballMatchPredictor.Domain.ViewModels.Withdrawing;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Mapping
{
    public class WithdrawingMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Withdrawing, WithdrawingViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.OutputAmount, src => src.OutputAmount)
                .Map(dest => dest.PaymentMethod, src => src.PaymentMethod.GetDisplayName())
                .Map(dest => dest.UserName, src => src.User.Username)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);
        }
    }
}
