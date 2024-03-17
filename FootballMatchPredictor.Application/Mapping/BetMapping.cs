using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Mapping
{
    public class BetMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Bet, BetViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Team1Name, src => src.Coefficient.Match.Team1.Name)
                .Map(dest => dest.Team2Name, src => src.Coefficient.Match.Team2.Name)
                .Map(dest => dest.CoefficientValue, src => src.Coefficient.CoefficientValue)
                .Map(dest => dest.CoefficientReferDescription, src => src.Coefficient.CoefficientRefer.Description)
                .Map(dest => dest.BetAmountMoney, src => src.BetAmountMoney)
                .Map(dest => dest.WinningAmount, src => src.WinningAmount)
                .Map(dest => dest.BetTypeName, src => src.BetType.TypeName)
                .Map(dest => dest.BetState, src => src.BetState.GetDisplayName())
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);
        }
    }
}
