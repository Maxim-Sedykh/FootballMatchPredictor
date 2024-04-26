using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.ViewModels.Bet;
using FootballMatchPredictor.Domain.ViewModels.Coefficient;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Mapping
{
    public class CoefficientMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Coefficient, CoefficientViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Team1Name, src => src.Match.Team1.Name)
                .Map(dest => dest.Team2Name, src => src.Match.Team2.Name)
                .Map(dest => dest.CoefficientValue, src => src.CoefficientValue)
                .Map(dest => dest.IsActive, src => src.IsActive)
                .Map(dest => dest.MatchDate, src => src.Match.MatchDate)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.BetType, src => src.BetType.GetDisplayName());

            config.NewConfig<Coefficient, MakeBetViewModel>()
                .Map(dest => dest.CoefficientId, src => src.Id);
        }
    }
}
