using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WebChatAssessment.CoreBusiness;
using WebChatAssessment.CoreBusiness.InterfaceDefinitions;
using WebChatAssessment.SignalR;

namespace WebChatAssessment.Bot;

public class QueryStockWorker : BackgroundService
{
    private readonly IHubContext<SignalRHub, ISignalRHub> hub;
    private readonly IStockResponseQueueBroker responseQueueBroker;

    public QueryStockWorker(IHubContext<SignalRHub, ISignalRHub> hub, IStockResponseQueueBroker responseQueueBroker)
    {
        this.hub = hub;
        this.responseQueueBroker = responseQueueBroker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await responseQueueBroker.ReceiveMessage();
            await hub.Clients.All.SendMessage("Bot", message);
        }
    } 
}