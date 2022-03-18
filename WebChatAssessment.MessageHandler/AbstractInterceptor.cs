using WebChatAssessment.CoreBusiness;
using WebChatAssessment.CoreBusiness.InterfaceDefinitions;

namespace WebChatAssessment.MessageHandler;

public abstract class AbstractInterceptor : IMessageInterceptor
{
    private readonly IMessageInterceptor nextInterceptor;

    protected AbstractInterceptor(IMessageInterceptor nextInterceptor)
    {
        this.nextInterceptor = nextInterceptor;
    }
 
    public virtual string Handle(string request)
    {
        return nextInterceptor.Handle(request);
    }
}