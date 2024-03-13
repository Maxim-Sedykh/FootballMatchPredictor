using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Domain.ViewModels.UserProfile
{
    /// <summary>
    /// Модель представления для получения статистики пользователя
    /// </summary>
    /// <param name="WinningSum"></param>
    /// <param name="BetsCount"></param>
    /// <param name="WinRate"></param>
    public record UserStatisticsViewModel(decimal WinningSum, int BetsCount, float WinRate);
}
