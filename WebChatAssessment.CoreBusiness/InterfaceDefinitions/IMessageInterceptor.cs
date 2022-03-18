namespace WebChatAssessment.CoreBusiness.InterfaceDefinitions;

public interface IMessageInterceptor
{ 
    string Handle(string message);
}