using FootballMatchPredictor.Application.Resources.Error;
using FootballMatchPredictor.Application.Resources.Success;
using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Domain.Enums;
using FootballMatchPredictor.Domain.Interfaces.Repository;
using FootballMatchPredictor.Domain.Interfaces.Services;
using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Country;
using FootballMatchPredictor.Domain.ViewModels.Team;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Xml.Linq;

namespace FootballMatchPredictor.Application.Services
{
    /// <inheritdoc/>
    public class TeamService : ITeamService
    {
        private readonly IBaseRepository<Team> _teamRepository;
        private readonly HttpClient _httpClient;

        public TeamService(IBaseRepository<Team> teamRepository, IHttpClientFactory httpClientFactory)
        {
            _teamRepository = teamRepository;
            _httpClient = httpClientFactory.CreateClient("MyHttpClient");
        }

        /// <inheritdoc/>
        public async Task<CollectionResult<TeamViewModel>> GetAllTeams()
        {
            var teams = await _teamRepository.GetAll().ToListAsync();

            var teamViewModels = teams.Select(x => x.Adapt<TeamViewModel>()).OrderBy(x => x.Id).ToList();

            if (teamViewModels.Count == 0)
            {
                return new CollectionResult<TeamViewModel>()
                {
                    ErrorMessage = ErrorMessage.TeamsNotFound,
                    ErrorCode = (int)StatusCode.TeamsNotFound
                };
            }

            return new CollectionResult<TeamViewModel>()
            {
                Data = teamViewModels,
                Count = teamViewModels.Count
            };
        }

        public async Task<CollectionResult<KeyValuePair<int,string>>> GetAllCountries()
        {
            var countryDictionary = await GetCountries();

            if (countryDictionary.Count == 0)
            {
                return new CollectionResult<KeyValuePair<int, string>>()
                {
                    ErrorMessage = ErrorMessage.CountriesNotFound,
                    ErrorCode = (int)StatusCode.CountriesNotFound
                };
            }

            return new CollectionResult<KeyValuePair<int, string>>()
            {
                Data = countryDictionary,
                Count = countryDictionary.Count
            };
        }

        public CollectionResult<KeyValuePair<short, string>> GetTeamsDictionary()
        {
            var teamDictionary = _teamRepository.GetAll().ToDictionary(k => k.Id, v => v.Id + " - " + v.Name);

            if (teamDictionary.Count == 0)
            {
                return new CollectionResult<KeyValuePair<short, string>>()
                {
                    ErrorMessage = ErrorMessage.CountriesNotFound,
                    ErrorCode = (int)StatusCode.CountriesNotFound
                };
            }

            return new CollectionResult<KeyValuePair<short, string>>()
            {
                Data = teamDictionary,
                Count = teamDictionary.Count
            };
        }

        public async Task<BaseResult> CreateTeam(CreateTeamViewModel viewModel)
        {
            var team = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Name == viewModel.TeamName);
            if (team != null) 
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TeamAlreadyExist,
                    ErrorCode = (int)StatusCode.TeamAlreadyExist
                };
            }

            var countryDictionary = await GetCountries();

            if (countryDictionary.Count == 0)
            {
                return new CollectionResult<KeyValuePair<int, string>>()
                {
                    ErrorMessage = ErrorMessage.CountriesNotFound,
                    ErrorCode = (int)StatusCode.CountriesNotFound
                };
            }

            Team newTeam = new Team()
            {
                Country = countryDictionary[viewModel.CountryIdentifier],
                Name = viewModel.TeamName,
                MatchesPlayed = 0,
                MatchesWon = 0,
            };

            await _teamRepository.CreateAsync(newTeam);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.TeamCreated
            };
        }

        private async Task<Dictionary<int, string>> GetCountries()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var countries = JsonConvert.DeserializeObject<List<CountryViewModel>>(data);

                var countryDictionary = countries
                    .Select((c, index) => new KeyValuePair<int, string>(index + 1, c.Name.Common + " " + c.Name.Official))
                    .ToDictionary(kv => kv.Key, kv => kv.Value);

                return countryDictionary;
            }

            return new Dictionary<int, string>();
        }

        public async Task<BaseResult> DeleteTeam(short id)
        {
            var team = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return new BaseResult()
                {
                    ErrorCode = (int)StatusCode.TeamNotFound,
                    ErrorMessage = ErrorMessage.TeamNotFound
                };
            }

            await _teamRepository.RemoveAsync(team);

            return new BaseResult()
            {
                SuccessMessage = SuccessMessage.TeamDeleted
            };
        }

        public async Task<BaseResult<TeamViewModel>> GetTeamData(short id)
        {
            var team = await _teamRepository.GetAll()
                .Select(x => new TeamViewModel()
                {
                    Id = x.Id,
                    TeamName = x.Name,
                    CountryName = x.Country,
                    MatchesPlayed = x.MatchesPlayed,
                    MatchesWon = x.MatchesWon
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return new BaseResult<TeamViewModel>()
                {
                    ErrorMessage = ErrorMessage.TeamNotFound,
                    ErrorCode = (int)StatusCode.TeamNotFound
                };
            }

            return new BaseResult<TeamViewModel>()
            {
                Data = team
            };
        }

        public async Task<BaseResult<TeamViewModel>> UpdateTeam(TeamViewModel viewModel)
        {
            var team = await _teamRepository.GetAll().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (team == null)
            {
                return new BaseResult<TeamViewModel>()
                {
                    ErrorCode = (int)StatusCode.TeamNotFound,
                    ErrorMessage = ErrorMessage.TeamNotFound
                };
            }

            var countryDictionary = await GetCountries();

            if (countryDictionary.Count == 0)
            {
                return new BaseResult<TeamViewModel>()
                {
                    ErrorMessage = ErrorMessage.CountriesNotFound,
                    ErrorCode = (int)StatusCode.CountriesNotFound
                };
            }

            team.Name = viewModel.TeamName;
            team.Country = countryDictionary[Convert.ToInt32(viewModel.CountryName)];
            team.MatchesPlayed = viewModel.MatchesPlayed;
            team.MatchesWon = viewModel.MatchesWon;

            await _teamRepository.UpdateAsync(team);

            return new BaseResult<TeamViewModel>()
            {
                SuccessMessage = SuccessMessage.TeamUpdated
            };
        }
    }
}
