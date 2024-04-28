using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Extensions;
using FootballMatchPredictor.Domain.ViewModels.Match;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Mapping
{
    public class MatchMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Match, MatchViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Team1Name, src => src.Team1.Name)
                .Map(dest => dest.Team2Name, src => src.Team2.Name)
                .Map(dest => dest.Team1GoalsCount, src => src.Team1GoalsCount)
                .Map(dest => dest.Team2GoalsCount, src => src.Team2GoalsCount)
                .Map(dest => dest.MatchState, src => src.MatchState.GetDisplayName())
                .Map(dest => dest.MatchDate, src => src.MatchDate);

            config.NewConfig<Match, UpdateMatchViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Team1, src => Convert.ToInt32(src.Team1.Id))
                .Map(dest => dest.Team2, src => Convert.ToInt32(src.Team2.Id))
                .Map(dest => dest.Team1GoalsCount, src => src.Team1GoalsCount)
                .Map(dest => dest.Team2GoalsCount, src => src.Team2GoalsCount)
                .Map(dest => dest.MatchState, src => src.MatchState.GetDisplayName())
                .Map(dest => dest.MatchDate, src => src.MatchDate);
        }
    }
}
