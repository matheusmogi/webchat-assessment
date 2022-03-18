namespace WebChatAssessment.CoreBusiness.InterfaceDefinitions;

public interface ISignalRHub
{
    Task SendMessage(string user, string message);
}