using Microsoft.AspNetCore.SignalR;
using WebChatAssessment.Bot;
using WebChatAssessment.CoreBusiness;
using WebChatAssessment.CoreBusiness.Contracts;
using WebChatAssessment.MessageHandler;
using WebChatAssessment.QueueBroker;

namespace WebChatAssessment.UI;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection collection)
    {
        collection.AddTransient<IStockShareService, StockShareService>();
        collection.AddTransient<IStockResponseQueueBroker, StockResponseQueueBroker>();
        collection.AddTransient<IMessageInterceptor>(ctx => new CommandInterceptor(new MessageInterceptor(default), (IStockShareService)ctx.GetService(typeof(IStockShareService))));
    }
}