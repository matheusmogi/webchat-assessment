using System.Text.RegularExpressions;
using WebChatAssessment.CoreBusiness.InterfaceDefinitions;

namespace WebChatAssessment.MessageHandler;

public class CommandInterceptor : AbstractInterceptor
{
    private readonly IStockShareService stockShareService;

    public CommandInterceptor(IMessageInterceptor messageInterceptor, IStockShareService stockShareService) : base(messageInterceptor)
    {
        this.stockShareService = stockShareService;
    }

    public override string Handle(string request)
    {
        if (!Regex.IsMatch(request, @"\A\/stock=\w*")) 
            return base.Handle(request);

        var code = Regex.Match(request, @"\A\/stock=(\D+)").Groups[1].Value;
        stockShareService.GetStockShareDetails(code);
        return string.Empty;
    }
}