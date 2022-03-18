namespace WebChatAssessment.CoreBusiness.Contracts;

public interface ISignalRHub
{
    Task SendMessage(string user, string message);
}