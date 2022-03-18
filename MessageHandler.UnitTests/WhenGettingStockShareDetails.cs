using System.Threading.Tasks;
using Flurl.Http.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using WebChatAssessment.CoreBusiness.InterfaceDefinitions;
using WebChatAssessment.MessageHandler;

namespace MessageHandler.UnitTests;

[TestFixture]
public class WhenGettingStockShareDetails
{
    private Mock<IStockResponseQueueBroker> stockResponseQueueBroker;
    private Mock<IConfiguration> configuration;
    private string baseUrl;
    private IStockShareService service;
    private HttpTest httpTest;

    [SetUp]
    public void Setup()
    {
        httpTest = new HttpTest();
        stockResponseQueueBroker = new Mock<IStockResponseQueueBroker>();
        configuration = new Mock<IConfiguration>();
        baseUrl = "https://baseurl.com/#code#";
        configuration.Setup(s => s["StockServiceUrl"]).Returns(baseUrl);
        service = new StockShareService(stockResponseQueueBroker.Object, configuration.Object);
    }

    [Test]
    public async Task ShouldReplaceUrlWithCodeReceived()
    {
        await service.GetStockShareDetails("code-request");

        httpTest.ShouldHaveCalled($"https://baseurl.com/code-request");
    }

    [Test]
    public async Task ShouldSendMessageToBroker()
    {
        await service.GetStockShareDetails("code-request");

        stockResponseQueueBroker.Verify(x => x.SendMessage(" quote is $$ per share"));
    }
}