namespace WebChatAssessment.CoreBusiness.InterfaceDefinitions;

public interface IStockShareService
{
    Task GetStockShareDetails(string code);
}