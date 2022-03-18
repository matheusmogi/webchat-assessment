using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using WebChatAssessment.CoreBusiness.InterfaceDefinitions;

namespace WebChatAssessment.QueueBroker;

public abstract class StockResponseQueueBroker : IStockResponseQueueBroker
{
    private readonly ServiceBusSender sender;
    private readonly ServiceBusReceiver receiver;
    private const string QueueName = "stock-response";

    protected StockResponseQueueBroker(IConfiguration configuration)
    {
        var serviceBusConnectionString = configuration["ConnectionStrings:ServiceBus"];
        var client = new ServiceBusClient(serviceBusConnectionString);
        sender = client.CreateSender(QueueName);
        receiver = client.CreateReceiver(QueueName);
    }

    public async Task SendMessage<T>(T message) where T : class
    {
        var serviceBusMessage = new ServiceBusMessage(new BinaryData(message));
        await sender.SendMessageAsync(serviceBusMessage);
    }

    public async Task<string> ReceiveMessage()
    {
        var message = await receiver.ReceiveMessageAsync();
        var content= message.Body.ToString();
        await receiver.CompleteMessageAsync(message);
        return content.Replace("\"",string.Empty);
    }
}