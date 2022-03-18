using Microsoft.AspNetCore.SignalR;
using WebChatAssessment.CoreBusiness;
using WebChatAssessment.CoreBusiness.Contracts;

namespace WebChatAssessment.SignalR;

public class SignalRHub : Hub<ISignalRHub>
{
    private readonly IMessageInterceptor interceptor;

    public SignalRHub(IMessageInterceptor interceptor)
    {
        this.interceptor = interceptor;
    }

    public async Task SendMessage(string user, string message)
    {
        var resultMessage = interceptor.Handle(message);
        await Clients.All.SendMessage( user, resultMessage);
    }
}