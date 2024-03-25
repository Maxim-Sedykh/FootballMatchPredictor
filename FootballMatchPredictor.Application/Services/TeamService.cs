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
            var teams = await _teamRepository.GetAll()
                .Select(x => x.Adapt<TeamViewModel>()).ToArrayAsync();

            if (teams.Length == 0)
            {
                return new CollectionResult<TeamViewModel>()
                {
                    ErrorMessage = ErrorMessage.TeamsNotFound,
                    ErrorCode = (int)StatusCode.TeamsNotFound
                };
            }

            return new CollectionResult<TeamViewModel>()
            {
                Data = teams,
                Count = teams.Length
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
                string data = response.Content.ReadAsStringAsync().Result;
                var countries = JsonConvert.DeserializeObject<List<CountryViewModel>>(data);



                var countryDictionary = countries.Select((c, index) =>
                    new
                    {
                        Key = index + 1,
                        Value = string.Concat(c.Name.Common, c.Name.Official)
                    }).ToDictionary(x => x.Key, x => x.Value);

                return countryDictionary;
            }

            return new Dictionary<int, string>();
        }
    }
}
