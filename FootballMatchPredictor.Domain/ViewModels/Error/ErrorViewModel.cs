namespace FootballMatchPredictor.Domain.ViewModels.Error
{
    public record ErrorViewModel(
            string ErrorMessage,
            int? StatusCode
        );
}
