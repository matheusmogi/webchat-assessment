namespace WebChatAssessment.CoreBusiness.InterfaceDefinitions;
 
public interface IStockResponseQueueBroker
{
    Task SendMessage<T>(T message) where T : class;
    Task<string> ReceiveMessage();
}