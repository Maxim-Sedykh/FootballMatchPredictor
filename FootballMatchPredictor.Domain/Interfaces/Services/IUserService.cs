using FootballMatchPredictor.Domain.Result;
using FootballMatchPredictor.Domain.ViewModels.Team;
using FootballMatchPredictor.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Получение информации по командам
        /// </summary>
        /// <returns></returns>
        Task<CollectionResult<UserViewModel>> GetAllUsers();

        /// <summary>
        /// Удаление пользователя по его индентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult> DeleteUser(long id);
    }
}
