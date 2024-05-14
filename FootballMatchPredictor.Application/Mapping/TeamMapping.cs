using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Application.Mapping
{
    public class TeamMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Team, TeamViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.TeamName, src => src.Name)
                .Map(dest => dest.CountryName, src => src.Country)
                .Map(dest => dest.MatchesPlayed, src => src.MatchesPlayed)
                .Map(dest => dest.MatchesWon, src => src.MatchesWon)
                .Map(dest => dest.Rating, src => Math.Round(src.Rating, 2));
        }
    }
}
