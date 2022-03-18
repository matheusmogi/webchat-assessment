using WebChatAssessment.Bot;
using WebChatAssessment.SignalR;
using WebChatAssessment.UI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.WebHost.ConfigureServices(service => { service.AddHostedService<QueryStockWorker>(); });
builder.Services.RegisterServices();

builder.Services.AddRazorPages();

var app = builder.Build();
app.MapHub<SignalRHub>("/signalr-hub");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();