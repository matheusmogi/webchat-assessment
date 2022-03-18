namespace WebChatAssessment.CoreBusiness.Contracts;

public interface IStockShareService
{
    Task GetStockShareDetails(string code);
}