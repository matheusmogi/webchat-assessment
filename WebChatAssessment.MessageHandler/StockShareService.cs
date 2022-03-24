using System.Text;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using WebChatAssessment.CoreBusiness.Contracts;

namespace WebChatAssessment.MessageHandler;

public class StockShareService : IStockShareService
{
    private readonly IStockResponseQueueBroker stockResponseBroker;
    private readonly string stockServiceUrl;

    public StockShareService(IStockResponseQueueBroker stockResponseBroker, IConfiguration configuration)
    {
        this.stockResponseBroker = stockResponseBroker;
        stockServiceUrl = configuration["StockServiceUrl"];
    }

    public async Task GetStockShareDetails(string code)
    {
        var stockUrlTransformed = stockServiceUrl.Replace("#code#", code);
        var stockResponse = await stockUrlTransformed.GetStreamAsync();
        var result = GetValuesFromStream(stockResponse);
        var responseMessage = CreateResponseMessage(result);
        await stockResponseBroker.SendMessage(responseMessage);
    }

    private static string CreateResponseMessage(IReadOnlyList<string> rows)
    {
        if (rows.Count < 2 || rows[1].Split(",").Length < 7)
            return "Stock not found";

        var row = rows[1];
        var result = row.Split(",");
        return $"{result?[0]} quote is $${result?[6]} per share";
    }

    private static List<string?> GetValuesFromStream(Stream stockResponse)
    {
        stockResponse.Position = 0;
        var rows = new List<string?>();
        using var reader = new StreamReader(stockResponse, Encoding.ASCII);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            rows.Add(line);
        }

        return rows;
    }
}