using Moq;
using NUnit.Framework;
using WebChatAssessment.CoreBusiness.Contracts;
using WebChatAssessment.MessageHandler;

namespace MessageHandler.UnitTests;

[TestFixture]
public class WhenMessageInterceptorInterceptsAMessage
{
    private MessageInterceptor messageInterceptor;

    [SetUp]
    public void Setup()
    {
        var commonMessageInterceptor = new Mock<IMessageInterceptor>();
        messageInterceptor = new MessageInterceptor(commonMessageInterceptor.Object);
    }
    [Test]
    public void ShouldCallBaseHandle()
    {
        const string message = "any kind of message";

       var response= messageInterceptor.Handle(message);

       Assert.AreEqual(message, response);
    }
}