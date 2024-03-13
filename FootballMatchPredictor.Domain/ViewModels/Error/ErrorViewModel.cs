namespace FootballMatchPredictor.Domain.ViewModels.Error
{
    /// <summary>
    /// Модель представления для предоставления информации об ошибке
    /// </summary>
    /// <param name="ErrorMessage"></param>
    /// <param name="StatusCode"></param>
    public record ErrorViewModel(
            string ErrorMessage,
            int? StatusCode
        );
}
