using WebChatAssessment.CoreBusiness.InterfaceDefinitions;

namespace WebChatAssessment.MessageHandler;

public class MessageInterceptor : AbstractInterceptor
{
    public MessageInterceptor(IMessageInterceptor messageInterceptor):base(messageInterceptor)
    {
    }
    public override string Handle(string request)
    {
        return request;
    }

}