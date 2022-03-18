namespace WebChatAssessment.CoreBusiness.Contracts;

public interface IMessageInterceptor
{ 
    string Handle(string message);
}