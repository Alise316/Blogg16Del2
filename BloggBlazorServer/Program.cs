using BlazorChat;
using BloggBlazorServer.Services;
using Microsoft.AspNetCore.ResponseCompression;     

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddScoped<UserStateService>();
builder.Services.AddHttpClient<SearchService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7018");
});




// Registrer HttpClient
builder.Services.AddScoped<HttpClient>(s =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7018") };
    return client;
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
    endpoints.MapHub<ChatHub>(ChatHub.HubUrl);
});

app.Run();