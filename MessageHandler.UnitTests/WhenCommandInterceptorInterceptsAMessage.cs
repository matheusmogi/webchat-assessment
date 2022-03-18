using Moq;
using NUnit.Framework;
using WebChatAssessment.CoreBusiness.InterfaceDefinitions;
using WebChatAssessment.MessageHandler;

namespace MessageHandler.UnitTests;

[TestFixture]
public class WhenCommandInterceptorInterceptsAMessage
{
    private Mock<IMessageInterceptor> commonMessageInterceptor;
    private Mock<IStockShareService> stockShareService;
    private CommandInterceptor commandInterceptor;


    [SetUp]
    public void Setup()
    {
        commonMessageInterceptor = new Mock<IMessageInterceptor>();
        stockShareService = new Mock<IStockShareService>();
        commandInterceptor = new CommandInterceptor(commonMessageInterceptor.Object, stockShareService.Object);
    }

    [Test]
    public void AndIsNotACommandShouldCallBaseHandle()
    {
        const string message = "any kind of message";

        commandInterceptor.Handle(message);

        commonMessageInterceptor.Verify(x => x.Handle(message));
    }


    [Test]
    public void AndIsACommandShouldCallStockShareService()
    {
        const string message = "/stock=aapl.us";

        commandInterceptor.Handle(message);

        stockShareService.Verify(x => x.GetStockShareDetails("aapl.us"));
    }
}