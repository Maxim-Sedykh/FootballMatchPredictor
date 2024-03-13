namespace FootballMatchPredictor.Domain.ViewModels.Error
{
    /// <summary>
    /// ������ ������������� ��� �������������� ���������� �� ������
    /// </summary>
    /// <param name="ErrorMessage"></param>
    /// <param name="StatusCode"></param>
    public record ErrorViewModel(
            string ErrorMessage,
            int? StatusCode
        );
}
